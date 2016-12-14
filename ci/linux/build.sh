# Fancy logging function
log() {
    printf "\n----------------------------------------------------------------"
    printf "  $1"
    printf "----------------------------------------------------------------\n"
}

# Build the project
log "Building project..."
dotnet build ./hhs-p6-webshop-project/project.json

# Build finished
log "Build finished."