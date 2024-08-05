### Building and running your application

### Deploying your application to the cloud

First, build your image.
If your cloud uses a different CPU architecture than your development
machine (e.g., you are on a Mac M1 and your cloud provider is amd64),
you'll want to build the image for that platform, e.g.:
`docker build --platform=linux/amd64 -t gogosumo-client:v1.0.0 .`.

Then, push it to your registry, e.g. `docker push gogosumocontainerregistry.azurecr.io/gogosumo-client:v1.0.0`.

Consult Docker's [getting started](https://docs.docker.com/go/get-started-sharing/)
docs for more detail on building and pushing.

### References

-   [Docker's .NET guide](https://docs.docker.com/language/dotnet/)
-   The [dotnet-docker](https://github.com/dotnet/dotnet-docker/tree/main/samples)
    repository has many relevant samples and docs.
