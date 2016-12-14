# Fancy logging function
log() {
    printf "\n\n%s\n" "----------------------------------------------------------------"
    printf "$1"
    printf "\n%s\n\n" "----------------------------------------------------------------"
}
# Set the proper execution permissions for the scripts
sudo chmod 777 ./init.sh
sudo chmod 777 ./setup.sh
sudo chmod 777 ./build.sh
sudo chmod 777 ./test.sh

# Initialize
./init.sh

# We're starting, show a status message
log "Starting CI build progress..."

# Set up the build environment
./setup.sh

# Build the project
./build.sh

# Test the project
./test.sh

# Build finished, show a status message
log "CI build progress finished."
