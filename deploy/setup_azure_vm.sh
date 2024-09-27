#! /bin/bash

################
# MANUALLY RUN #
################

##### MANUALLY RUN ON VM
# cd ~
# sudo apt-get update
# sudo apt-get -y install git
# git clone https://github.com/DillonWallOrg/GoGoSumo.git

##### MANUALLY COPY SECRETS AND CERTS
## copy certs from /etc/ssl/ into ~/GoGoSumo/deploy
## copy .env.prod from local ~/GoGoSumo/deploy to server ~/GoGoSumo/deploy


################
# SETUP SCRIPT #
################

### Convenience aliases
echo "alias gogoup=\"sudo docker compose -f ~/GoGoSumo/compose.prod.yml --env-file ~/GoGoSumo/deploy/.env.prod up -d\"" >> ~/.bashrc
echo "alias gogodown=\"sudo docker compose -f ~/GoGoSumo/compose.prod.yml --env-file ~/GoGoSumo/deploy/.env.prod down\"" >> ~/.bashrc
source ~/.bashrc

### Install Docker
sudo install -m 0755 -d /etc/apt/keyrings
sudo curl -fsSL https://download.docker.com/linux/ubuntu/gpg -o /etc/apt/keyrings/docker.asc
sudo chmod a+r /etc/apt/keyrings/docker.asc
echo \
  "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.asc] https://download.docker.com/linux/ubuntu \
  $(. /etc/os-release && echo "$VERSION_CODENAME") stable" | \
  sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
sudo apt-get update
sudo apt-get -y install docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin

### Create the docker group and add the current user to it
sudo groupadd docker
sudo usermod -aG docker $USER
sudo chgrp docker /var/run/docker.sock
