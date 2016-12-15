#!/usr/bin/env bash

# Fancy logging function
log() {
    printf "\n\n%s\n" "----------------------------------------------------------------"
    printf "$1"
    printf "\n%s\n\n" "----------------------------------------------------------------"
}

# Build the project
log "Building project..."
dotnet build ./hhs-p6-webshop-project/project.json

# Build finished
log "Build finished."