#! /bin/bash

# :README:
# Run the following commands manually to set up git and clone the repo, then navigate to the deploy directory and execute this script
# sudo apt-get update
# sudo apt-get install git
# git clone https://github.com/DillonWall/GoGoSumo.git

# :README:
# Run this command to copy the env.prod file from your local machine to the VM
#   (assumes you have the .pem file in the dir: ~/.ssh/gogosumo_vm/ with 700 permissions)
# scp -i ~/.ssh/gogosumo_vm/GoGoSumo-VM-Main_key.pem .env.prod azureuser@20.2.233.181:/home/azureuser/GoGoSumo/deploy/.env.prod

# Add Docker's official GPG key:
# sudo apt-get install ca-certificates curl
sudo install -m 0755 -d /etc/apt/keyrings
sudo curl -fsSL https://download.docker.com/linux/ubuntu/gpg -o /etc/apt/keyrings/docker.asc
sudo chmod a+r /etc/apt/keyrings/docker.asc

# Add the repository to Apt sources:
echo \
  "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.asc] https://download.docker.com/linux/ubuntu \
  $(. /etc/os-release && echo "$VERSION_CODENAME") stable" | \
  sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
sudo apt-get update

sudo apt-get -y install docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin