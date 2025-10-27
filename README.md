# CourierFleet

Para testar o projeto sera necessario ter o Docker instalado na sua maquina. Abra um novo terminal no docker e execute o seguinte comando para incializar o Rabbit:
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:4-management
Apos isso, abra o terminal novamente no diretório onde consta o arquivo docker-compose.yml e execute o comando para inciar o banco de dados do postgress:
docker-compose up -d
o usuario e senha estao no proprio arquivo. 
Abra a api e execute as migrations para criar as tabelas no banco de dados para poder efetuar os CRUDs:
dotnet ef database update --startup-project "C:seu diretorio onde clonou o projeto\CourierFleetApi\CourierFleetApi.csproj" --project "C:seu diretorio onde clonou o projeto\CourierFleetInfrastructure\CourierFleetInfrastructure.csproj"
