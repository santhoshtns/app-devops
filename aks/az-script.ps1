$resourceGroup = 'shopping-rg'
$registryname = 'shoppacr'
$acrfullname = 'shoppacr.azurecr.io'
$aksname = 'shopp-aks'
$nodes = 1

$account = az account show
$account

# Create Resource group
az group create --name $resourceGroup --location westeurope

# create azure container registry
az acr create --resource-group $resourceGroup --name $registryname --sku Basic

# enable admin account on registry
az acr update -n $registryname --admin-enabled true

# login to acr
az acr login --name $registryname 

# get acr from resource group
$acrFullName = az acr list --resource-group $resourceGroup --query "[].{acrLoginServer:loginServer}" --output table
$acrFullName[2]

$shoppingapiTag = $acrFullName[2] + '/shoppingapi:v1'
$shoppingclientTag = $acrFullName[2] + '/shoppingclient:v1'

# tag the images to acr.io
docker tag shoppingapi:latest $shoppingapiTag
docker tag shoppingclient:latest $shoppingclientTag

# push the docker images
docker push $shoppingapiTag
docker push $shoppingclientTag

# list the repositories
az acr repository list --name $registryname --output table

# show tags
az acr repository show-tags --name $registryname --repository shoppingclient --output table
az acr repository show-tags --name $registryname --repository shoppingapi --output table

# create aks and attach to acr.this will take few mins
az aks create --resource-group $resourceGroup --name $aksname --node-count $nodes --generate-ssh-keys --attach-acr $registryname

# install az cli for aks
az aks intall-cli

# get context
az aks get-credentials --resource-group $resourceGroup --name $aksname

# create secrets for docker related creds
# password might be wrong
kubectl create secret docker-registry acr-secret --docker-server=$acrfullname --docker-username=$registryname --docker-password=jxRN05QKSge2yXx5Tb=YT64Zfx2 --docker-email=san.tn@gmail.com

# scale up pods manually
kubectl scale --replicas=3 deployment.apps/shoppingclient-deployment

# scale down pods manually
kubectl scale --replicas=1 deployment.apps/shoppingclient-deployment