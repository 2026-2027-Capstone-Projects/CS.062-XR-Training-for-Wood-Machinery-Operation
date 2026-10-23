# Find the Unity Editor installed through Unity Hub
$unityRoot = "C:\Program Files\Unity\Hub\Editor"

if (-not (Test-Path $unityRoot)) {
    Write-Error "Unity Hub Editor directory not found: $unityRoot"
    exit 1
}

# Find the newest installed Unity Editor
$unityEditor = Get-ChildItem $unityRoot -Directory |
    Sort-Object Name -Descending |
    Select-Object -First 1

if (-not $unityEditor) {
    Write-Error "No Unity Editor installations found."
    exit 1
}

$yamlMerge = Join-Path `
    $unityEditor.FullName `
    "Editor\Data\Tools\UnityYAMLMerge.exe"

if (-not (Test-Path $yamlMerge)) {
    Write-Error "UnityYAMLMerge not found: $yamlMerge"
    exit 1
}

Write-Host "Using UnityYAMLMerge:"
Write-Host $yamlMerge

# Configure this repository's local Git config
git config --local merge.tool unityyamlmerge

git config --local mergetool.unityyamlmerge.trustExitCode false

$command = "`"$yamlMerge`" merge -p `"$BASE`" `"$REMOTE`" `"$LOCAL`" `"$MERGED`""

git config --local mergetool.unityyamlmerge.cmd $command

Write-Host ""
Write-Host "UnityYAMLMerge configured for this repository."
Write-Host "Run 'git mergetool' when resolving conflicts."