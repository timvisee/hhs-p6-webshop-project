# Fancy logging function
log() {
    echo "----------------------------------------------------------------"
    echo "  $1"
    echo "----------------------------------------------------------------"
}

# Build the project
log "Building project..."
dotnet build ../../hhs-p6-webshop-project/project.json

# Build finished
log "Build finished."