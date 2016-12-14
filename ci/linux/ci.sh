# Fancy logging function
log() {
    echo "----------------------------------------------------------------"
    echo "  $1"
    echo "----------------------------------------------------------------"
}

# We're starting, show a status message
log "Starting CI build progress..."

# Set up the build environment
./setup.sh

# Build the project
./build.sh

# Build finished, show a status message
log "CI build progress finished."
