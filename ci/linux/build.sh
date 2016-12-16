#!/usr/bin/env bash

# Fancy logging function
log() {
    printf "\n\n%s\n" "----------------------------------------------------------------"
    printf "$1"
    printf "\n%s\n\n" "----------------------------------------------------------------"
}

# Build the project, and validate the exit code
log "Building project..."
dotnet build ./hhs-p6-webshop-project/project.json
rc=$?; if [[ $rc != 0 ]]; then exit $rc; fi

# Run the project to set up the test database, and validate the exit code
log "Running project to set up the initial database..."
cd hhs-p6-webshop-project

dotnet run -- --force-database-initialization --exit-after-initialization
rc=$?; if [[ $rc != 0 ]]; then exit $rc; fi

cd ..

# Build finished
log "Build finished."