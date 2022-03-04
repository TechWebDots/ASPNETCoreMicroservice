# ASPNETCoreMicroservice
ASP.NET Core Microservice with C#
Docker/dotnet Command Used:
docker images
docker ps
dotnet restore
dotnet publish -o obj/Docker/publish
docker build -t coreappimage .
docker run -d -p 8001:83 --name core1 coreappimage
Case 1: Run the same image in multiple containers
We can run the same image in multiple containers at the same time by using:
docker run -d -p 8002:83 --name core2 coreappimage
docker run -d -p 8003:83 --name core3 coreappimage
Case 2: Manage Containers: Stop/Start/Remove Containers
Stop container:
We can stop any running containers using “docker stop containerid/containername”
docker stop core1
docker ps -a
docker start core1
Remove Container:
We can remove any stopped container, but then we will not be able to start it again.
So first we should stop the container before removing it:
docker stop core1
docker rm core1
Case 3: Run the same image in multiple environments
create an image by repeating the same steps (dotnet restore, dotnet publish and docker build)
docker run -d -p 8004:83 --name core4 coreappimage
Run Image in a specific environment:(Check the code changes for IAppSettings)
docker run -d -p 8005:83 --name core5 --env ASPNETCORE_ENVIRONMENT=Development coreappimage
docker run -d -p 8006:83 --name core6 --env ASPNETCORE_ENVIRONMENT=QA coreappimage
Case 4: Tag and Run image with different versions
Let’s remove all running containers:
docker stop core2 core3 core4 core5 core6
docker rm core2 core3 core4 core5 core6
docker ps  (Note: There is no running container here.)
Step 1: Tag previously created image “coreappimage” with version 1.0.
Syntax: docker tag imagename imagename :version
docker tag coreappimage coreappimage:1.0
Step 2: Update the application to apply the authorization filter so that the API will be accessible using the Authorization Header “auth_key:core.”
(Check the code changes for SecurityHeadersAttribute)
docker tag coreappimage coreappimage:1.1
Run image with version 1.0:
docker run -d -p 8001:83 --name core1 --env ASPNETCORE_ENVIRONMENT=QA coreappimage:1.0
Run image with version 1.1:
docker run -d -p 8002:83 --name core2 --env ASPNETCORE_ENVIRONMENT=QA coreappimage:1.1

Note: Here we can see that for version 1.0 of the image, no authorization is required for the API or values. 
However, for version 1.1 of the image, we have to pass an authorization header. 
