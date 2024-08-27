#! /bin/bash

##### MANUALLY RUN ON VM
# sudo apt-get update
# sudo apt-get install git
# git clone https://github.com/DillonWall/GoGoSumo.git
### Setup SSL cert
# sudo apt install snapd
# sudo snap install --classic certbot
# sudo ln -s /snap/bin/certbot /usr/bin/certbot
# sudo certbot certonly --standalone
# cd ~/GoGoSumo/deploy
# sudo cp /etc/letsencrypt/live/gogosumoapi.eastasia.cloudapp.azure.com/fullchain.pem ./fullchain.pem
# sudo cp /etc/letsencrypt/live/gogosumoapi.eastasia.cloudapp.azure.com/privkey.pem ./privkey.pem

##### MANUALLY RUN ON LOCAL (LINUX) MACHINE FROM "GoGoSumo/deploy/" (copy secrets from local machine to vm)
# scp -i ~/.ssh/gogosumo_vm/GoGoSumo-VM-Main_key.pem .env.prod azureuser@20.2.233.181:/home/azureuser/GoGoSumo/deploy/.env.prod




### Install Docker
# Add Docker's official GPG key:
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