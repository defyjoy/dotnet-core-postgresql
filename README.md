# dotnet-core-postgresql

A sample MVC project about how to use PostgreSQL with ASP.NET Core.

You can take a look [Medium post](https://medium.com/@isikabdurrahman/net-core-ile-postgresql-kullan%C4%B1m%C4%B1-7aa025ec9123) for detailed instructions [TR]

This project uses;
- .NET 5.0 target framework.
- Npgsql.EntityFrameworkCore.PostgreSQL 5.0.2 NuGet package.

## Project Setup

### Clone repo


    $ git clone https://github.com/abdurrahman/dotnet-core-postgresql.git


### Update appsettings.json

Configure connection string in project's appsettings.json, replacing the `username`, `password`, and `dbname` appropriately (Consider to change Server name if it necessary):


    "ConnectionStrings": {
        "DefaultConnection": "User ID=username;Password=password;Server=localhost;Port=5432;Database=dbname;Integrated Security=true;Pooling=true;"
    },


## Bastion Host operations - 
---

### Login into bastion server - 

    $ ssh -i key.pem ec2-user@<ip address>

### Install dotnet into bastion - 

    $  wget https://dot.net/v1/dotnet-install.sh 

### Run the installation script - 

    $ ./dotnet-install.sh --version 5.0.404  --install-dir ~/.dotnet

### Add the path to .bashrc 

    $ echo "export PATH=$PATH:$HOME/.dotnet" >> ~/.bashrc


### install dotnet ef tools - 

    $ dotnet tool install --global dotnet-ef

### Running the migration 

    Execute the following comment inside the project directory, **where the .csproj file is located. inside src directory **:
    $ dotnet ef database update


## NOTE : All above commands need to be run from bastion host within subnet

---

## AWS Commands to check the registries - 

### After running the migration, the database is created and web application is ready to be run. 


### list your ecr registries - 

    $ aws ecr describe-repositories

### The github actions would push the items into ECR verify using ecr command - 

    choose the repositoryName from previous step

    $ aws ecr list-images --repository-name <ecr name>

### update local kubeconfig file - 

    $ aws eks update-kubeconfig --name <cluster name> --region us-east-1

### Install kubectl -

    $ curl -LO "https://dl.k8s.io/release/$(curl -L -s https://dl.k8s.io/release/stable.txt)/bin/linux/amd64/kubectl"

### Download and install helm  - 

    $ curl -fsSL -o get_helm.sh https://raw.githubusercontent.com/helm/helm/main/scripts/get-helm-3
    $ chmod 700 get_helm.sh
    $ ./get_helm.sh


### Deploy the application using helm 

    $ helm upgrade -i dotnet-postgresql ./  


### Run the application from localhost - 

    since the application is running on kubernetes you need to port forward it. The below command will forward the port 80(container) to port 8080(host).

    $ kubectl port-forward svc/dotnet-postgresql 8080:80

### You can now run the application using - 
    
    http://localhost:8080

### To check if the data is pushed inside postgresql. login into 

    $ psql -h <rds endpoint> -p 5432 -d <database name> -U <user name> 


### Check the table AspNetUsers -

    $ olien => SELECT * FROM "AspNetUsers";

    this should return you a table with list of users registered from the portal running above - http://localhost:8080

