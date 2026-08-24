"# IrongridC2System" 

## פקודות 
Windows PowerShell
```
docker compose up -d
```
```
docker ps
```
```
get-Content seed_database.sql | docker exec -i db mysql -uroot -proot testDb
```
### צפייה ב-mysql
```
docker exec -it db mysql -uroot -proot
```
```
use testDb;
```
```
select * from Assets;
```
```
select * from Units;
```
```
describe AssetLiveStatus;
```
```
exit;
```
### הפעלת producer
```
cd producer
```
```
dotnet run
```
```
docker exec -it broker bash
```
```
/opt/kafka/bin/kafka-topics.sh --bootstrap-server localhost:9092 --list
```
```
/opt/kafka/bin/kafka-console-consumer.sh --topic PerimeterSensor-topic --bootstrap-server localhost:9092 --from-beginning
```
ctrl+c
```
opt/kafka/bin/kafka-console-consumer.sh --topic UAV-topic --bootstrap-server localhost:9092 --from-beginning
```
ctrl+c
```
exit
```
### הפעלת consumer
```
cd ..
```
```
cd consumer
```
```
dotnet run
```
ctrl+c
```
docker exec -it db mysql -uroot -proot
```
```
use testDb;
```
```
select * from AssetLiveStatus;
```
```
exit;
```
### הפעלת API
```
cd ..
```
```
cd .\IronGridAPI\
```
```
dotnet watch run
```
