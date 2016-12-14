# Fancy logging function
log() {
    echo "----------------------------------------------------------------"
    echo "  $1"
    echo "----------------------------------------------------------------"
}

# Set up the build environment
log "Setting up build environment...\nSetting up dotnet repository..."

# Set up the dotnet repository
sudo sh -c 'echo "deb [arch=amd64] https://apt-mo.trafficmanager.net/repos/dotnet-release/ trusty main" > /etc/apt/sources.list.d/dotnetdev.list'
sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 417A0893

# Update all repositories
log "Updating all repositories..."
sudo apt-get update

# Install dotnet
log "Installing dotnet..."
sudo apt-get -y install dotnet-dev-1.0.0-preview2.1-003177

# Install project dependencies
log "Installing project dependencies..."
dotnet restore

# Build environment configured
log "Build environment configured."