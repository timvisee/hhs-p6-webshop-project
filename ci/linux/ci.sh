# Fancy logging function
log() {
    printf "\n%s\n" "----------------------------------------------------------------"
    printf "$1"
    printf "\n%s\n" "----------------------------------------------------------------"
}

# We're starting, show a status message
log "Starting CI build progress..."

# Set the proper execution permissions for the scripts
sudo chmod 777 ./setup.sh
sudo chmod 777 ./build.sh

# Set up the build environment
./setup.sh

# Build the project
./build.sh

# Build finished, show a status message
log "CI build progress finished."
