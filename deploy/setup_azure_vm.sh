#! /bin/bash

### MANUALLY RUN ON VM
# sudo apt-get update
# sudo apt-get install git
# git clone https://github.com/DillonWall/GoGoSumo.git

### MANUALLY RUN ON LOCAL LINUX MACHINE (copy secrets from local machine to vm)
# scp -i ~/.ssh/gogosumo_vm/GoGoSumo-VM-Main_key.pem .env.prod azureuser@20.2.233.181:/home/azureuser/GoGoSumo/deploy/.env.prod
# scp -i ~/.ssh/gogosumo_vm/GoGoSumo-VM-Main_key.pem cert.pfx azureuser@20.2.233.181:/home/azureuser/GoGoSumo/deploy/cert.pfx

### Only run this when setting up a real SSL certificate, don't need to do this with a self-signed dev cert
# sudo apt-get -y install ca-certificates curl
# # Setup HTTPS certificate
# openssl pkcs12 -in ./deploy/cert.pfx -clcerts -nokeys -out gogosumo_cacert.crt
# sudo cp gogosumo_cacert.crt /usr/local/share/ca-certificates/
# sudo update-ca-certificates
# rm gogosumo_cacert.crt

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