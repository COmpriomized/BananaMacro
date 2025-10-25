#!/usr/bin/env bash
PROJECT_PATH="Plugins/Samples/SamplePlugin"
OUTPUT_INSTALL="Plugins/Installed"

dotnet build "$PROJECT_PATH" -c Release
BIN_PATH="$PROJECT_PATH/bin/Release/net6.0"
DLL_PATH="$BIN_PATH/SamplePlugin.dll"

if [ ! -f "$DLL_PATH" ]; then
  echo "Built DLL not found: $DLL_PATH"
  exit 1
fi

mkdir -p "$OUTPUT_INSTALL"
cp "$DLL_PATH" "$OUTPUT_INSTALL/"
echo "Copied $DLL_PATH to $OUTPUT_INSTALL"