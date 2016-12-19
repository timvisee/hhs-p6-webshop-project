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
rc=$?
if [[ $rc != 0 ]]
then
    log "Building project failed! Exited with code $rc"
    exit $rc
fi

# Run the project to set up the test database, and validate the exit code
log "Running project to set up the initial database...\n$ app --force-database-initialization --exit-after-initialization"
cd hhs-p6-webshop-project

dotnet run -- --force-database-initialization --exit-after-initialization
rc=$?
if [[ $rc != 0 ]]
then
    log "Building project failed! Exited with code $rc"
    exit $rc
fi

cd ..

# Build finished
log "Build finished."