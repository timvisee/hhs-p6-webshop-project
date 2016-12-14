# Fancy logging function
log() {
    printf "\n%s\n" "----------------------------------------------------------------"
    printf "$1"
    printf "\n%s\n" "----------------------------------------------------------------"
}

# Test the project
log "Testing project..."
dotnet test ./hhs-p6-webshop-project/project.json

# Test finished
log "Test finished."
