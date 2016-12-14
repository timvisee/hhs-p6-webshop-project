#!/usr/bin/env bash

# Fancy logging function
log() {
    printf "\n\n%s\n" "----------------------------------------------------------------"
    printf "$1"
    printf "\n%s\n\n" "----------------------------------------------------------------"
}

# Test the project
log "Testing project..."
dotnet test ./hhs-p6-webshop-project/project.json

# Test finished
log "Test finished."
