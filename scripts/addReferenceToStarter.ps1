param(
    [string]$ModuleName,
    [string]$BaseMauiProjectRelativePath,
    [string]$BuildPropsFileName,
    [string]$ProjectSuffixesString, # <--- CAMBIO: Recibe como string
    [string]$SolutionDir
)

# Convertir el string de sufijos a un array
$ProjectSuffixes = $ProjectSuffixesString.Split(',') | ForEach-Object { $_.Trim() }
$AbsoluteSolutionDir = (Resolve-Path -Path $SolutionDir -ErrorAction Stop).Path

Write-Host "Sufijos de proyecto a procesar: $($ProjectSuffixes -join ', ')" # Muestra los sufijos procesados


Write-Host "Iniciando script para gestionar referencias y props para el módulo: $ModuleName"

# --- Configurar Rutas Absolutas ---
$propsFilePath = Join-Path $AbsoluteSolutionDir $BuildPropsFileName
$baseMauiProjectPath = Join-Path $AbsoluteSolutionDir $BaseMauiProjectRelativePath
$baseMauiProjectDir = Split-Path $baseMauiProjectPath -Parent

Write-Host "Ruta Build.props: $propsFilePath"
Write-Host "Ruta BaseMAUI.csproj: $baseMauiProjectPath"

# --- 1. Actualizar/Crear Build.props ---
try {
    Write-Host "Procesando $BuildPropsFileName..."
    $msbuildPropertyForModule = "$($ModuleName)Module" # Ej: PaysModule

    if (Test-Path $propsFilePath) {
        [xml]$propsXml = Get-Content $propsFilePath
    } else {
        Write-Host "El archivo $BuildPropsFileName no existe, creándolo."
        [xml]$propsXml = "<Project></Project>"
    }

    $propertyGroupNode = $propsXml.Project.PropertyGroup
    if (-not $propertyGroupNode) {
        $propertyGroupNode = $propsXml.CreateElement("PropertyGroup", $propsXml.Project.NamespaceURI)
        $propsXml.Project.AppendChild($propertyGroupNode) | Out-Null
    }

    # Asegurar que solo haya un PropertyGroup si se acaba de crear el nodo Project.
    # Si hay múltiples, tomamos el primero (o puedes añadir lógica para seleccionar uno específico).
    if ($propertyGroupNode -is [array]) { $propertyGroupNode = $propertyGroupNode[0] }


    $existingProp = $propertyGroupNode.SelectSingleNode("./*[local-name()='$msbuildPropertyForModule']")
    if (-not $existingProp) {
        Write-Host "Añadiendo propiedad '$msbuildPropertyForModule' a $BuildPropsFileName."
        $newProp = $propsXml.CreateElement($msbuildPropertyForModule, $propsXml.Project.NamespaceURI)
        $newProp.InnerText = "1" # Valor por defecto para activar el módulo
        $propertyGroupNode.AppendChild($newProp) | Out-Null
    } else {
        Write-Host "La propiedad '$msbuildPropertyForModule' ya existe en $BuildPropsFileName. Valor actual: $($existingProp.InnerText)"
        # Opcional: Actualizar valor si es necesario, ej: $existingProp.InnerText = "1"
    }

    $propsXml.Save($propsFilePath)
    Write-Host "$BuildPropsFileName guardado exitosamente."

} catch {
    Write-Error "Error procesando ${BuildPropsFileName}: $_"
    exit 1
}

# --- 2. Actualizar BaseMAUI.csproj con ProjectReferences Condicionales ---
try {
    Write-Host "Procesando $baseMauiProjectPath para añadir ProjectReferences..."
    if (-not (Test-Path $baseMauiProjectPath)) {
        Write-Error "No se encontró el archivo $baseMauiProjectPath."
        exit 1
    }
    [xml]$mauiProjectXml = Get-Content $baseMauiProjectPath

    # Encontrar o crear un ItemGroup para ProjectReferences
    $itemGroupNode = $mauiProjectXml.Project.ItemGroup | Where-Object { $_.ProjectReference } | Select-Object -First 1
    if (-not $itemGroupNode) {
        $itemGroupNode = $mauiProjectXml.CreateElement("ItemGroup", $mauiProjectXml.Project.NamespaceURI)
        $mauiProjectXml.Project.AppendChild($itemGroupNode) | Out-Null
    }




    Write-Host "Procesando <DefineConstants> en $BuildPropsFileName..."
    $defineConstantKeyPrefix = "$($ModuleName)Mod"  # Ej: PaysMod
    # $msbuildPropertyForModule ya está definido (ej. PaysModule)
    $newDefineStringSegment = "$($defineConstantKeyPrefix)`$($msbuildPropertyForModule)" # Ej: PaysMod$(PaysModule) (el ` escapa el $ para que sea literal)
    $defineConstNode = $propertyGroupNode.SelectSingleNode("./DefineConstants")

    $currentDefinesText = if ($isNewDefineConstantsNode) { "" } else { $defineConstNode.InnerText }
    Write-Host "Valor actual (o inicial si es nuevo nodo) de <DefineConstants>: '$currentDefinesText'"

    # Convertir el texto actual a una lista de defines, excluyendo $(DefineConstants) temporalmente para la lógica
    $definesList = New-Object System.Collections.Generic.List[string]
    if (-not [string]::IsNullOrWhiteSpace($currentDefinesText)) {
        $currentDefinesText.Split(';') | ForEach-Object {
            $trimmedItem = $_.Trim()
            # Añadir a la lista solo si no está vacío Y no es $(DefineConstants) en sí mismo
            if ((-not [string]::IsNullOrWhiteSpace($trimmedItem)) -and ($trimmedItem -ne '$(DefineConstants)')) {
                $definesList.Add($trimmedItem)
            }
        }
    }

    # Verificar si nuestro nuevo segmento (basado en el prefijo) ya existe en la lista
    $segmentAlreadyExists = $false
    foreach ($item in $definesList) {
        if ($item.StartsWith($defineConstantKeyPrefix)) {
            $segmentAlreadyExists = $true
            Write-Host "El segmento clave '$defineConstantKeyPrefix' (como '$item') ya existe en DefineConstants."
            break
        }
    }

    if (-not $segmentAlreadyExists) {
        Write-Host "Añadiendo '$newDefineStringSegment' a la lista de DefineConstants."
        $definesList.Add($newDefineStringSegment)
    }

    # Reconstruir el string de DefineConstants, asegurando que $(DefineConstants) esté al principio
    # y que los elementos estén separados por punto y coma.
    $finalDefinesString = '$(DefineConstants)'
    if ($definesList.Count -gt 0) {
        $finalDefinesString += ';' + ($definesList -join ';')
    }
    
    # Evitar $(DefineConstants); si no hay otras constantes
    if ($finalDefinesString -eq '$(DefineConstants);') {
        $finalDefinesString = '$(DefineConstants)'
    }

    $defineConstNode.InnerText = $finalDefinesString
    Write-Host "Valor final de <DefineConstants>: $($defineConstNode.InnerText)"
    
    # Guardar el archivo Build.props (esto ya estaba al final del bloque try)
    $propsXml.Save($propsFilePath)





    foreach ($suffix in $ProjectSuffixes) {
        $newProjectName = "$($ModuleName)$suffix" # Ej: PaysDomain, PaysPresentation
        # Asumiendo la estructura de tu plantilla anterior donde projectName es Modulo+Presentation
        # Para los otros, el nombre del proyecto y la carpeta coinciden con NewProjectName
        # La ruta del proyecto relativo a la raíz de la solución
        $newProjectSolutionRelativePath = "Modules/$ModuleName/$newProjectName/$newProjectName.csproj"
        if ($suffix -eq "Presentation") { # Caso especial para el de presentación si usas el símbolo projectName de antes
             # Si projectName (ej: PaysPresentation) ya incluye el sufijo, no lo añadas de nuevo
             # y si la carpeta también se llama PaysPresentation
             $presentationProjectNameSymbol = "$($ModuleName)Presentation" # Esto es lo que tu símbolo projectName debería resolver
             $newProjectSolutionRelativePath = "Modules/$ModuleName/$presentationProjectNameSymbol/$presentationProjectNameSymbol.csproj"
        }
        $baseUriPath = if ($baseMauiProjectDir.EndsWith('\') -or $baseMauiProjectDir.EndsWith('/')) { $baseMauiProjectDir } else { "$baseMauiProjectDir\" }
        $baseUri = [System.Uri]$baseUriPath
        $newProjectAbsolutePath = Join-Path $AbsoluteSolutionDir $newProjectSolutionRelativePath

         $targetUri = New-Object System.Uri($newProjectAbsolutePath, [System.UriKind]::Absolute)
        $relativeUri = $baseUri.MakeRelativeUri([System.Uri]$targetUri)
        $relativePathToNewProject = [System.Uri]::UnescapeDataString($relativeUri.OriginalString)
        $relativePathToNewProject = $relativePathToNewProject.Replace('/', '\')

        $conditionValue = "'`$($($ModuleName)Module)' == '1'" # Ej: '$(PaysModule)' == '1'


        # Verificar si ya existe una referencia similar (por path y condición)
        $existingRef = $itemGroupNode.SelectSingleNode("./ProjectReference[@Include=""$relativePathToNewProject"" and @Condition=""$conditionValue""]")

        if ($existingRef) {
            Write-Host "ProjectReference para '$newProjectName' con la condición '$conditionValue' ya existe. Omitiendo."
            continue
        }
        # O verificar solo por path si no quieres duplicados sin importar la condición
        $existingRefByPath = $itemGroupNode.SelectSingleNode("./ProjectReference[@Include='$relativePathToNewProject']")
        if ($existingRefByPath) {
            Write-Host "ProjectReference para '$newProjectName' (path: $relativePathToNewProject) ya existe (condición diferente o sin condición). Omitiendo para evitar duplicados."
            continue
        }


        Write-Host "Añadiendo ProjectReference para '$newProjectName' a $baseMauiProjectPath con condición: $conditionValue"
        $projectRefElement = $mauiProjectXml.CreateElement("ProjectReference", $mauiProjectXml.Project.NamespaceURI)
        $projectRefElement.SetAttribute("Condition", $conditionValue)
        $projectRefElement.SetAttribute("Include", $relativePathToNewProject)


        $itemGroupNode.AppendChild($projectRefElement) | Out-Null
    }

    $mauiProjectXml.Save($baseMauiProjectPath)
    Write-Host "$baseMauiProjectPath guardado exitosamente."

} catch {
    Write-Error "Error procesando ${baseMauiProjectPath}: $_"
    exit 1
}

Write-Host "Script finalizado."