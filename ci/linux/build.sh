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

# Run the project to set up the test database
log "Running project to set up the initial database..."
cd hhs-p6-webshop-project
dotnet run -- --force-database-initialization --exit-after-initialization
cd ..

# Build finished
log "Build finished."