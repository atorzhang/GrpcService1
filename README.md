# gRPC调用Demo

### 该demo使用了如下依赖

项目 | 说明
-|-|-
Autofac| 将Iservice项目和Service项目做自动注入处理 |
AutoMapper| 在Ext文件夹AutoMapperProfiles下配置proto自动生成的类和实体dto映射关系 |
Grpc.AspNetCore| 配置客户端和服务端的Protos文件夹下的.proto文件，服务端：GrpcServices="Server"，客户端：GrpcServices="Client" |
Microsoft.AspNetCore.Grpc.HttpApi| 启用Http方法访问grpc接口，google文件夹下的文件为必要支持文件，除此外在proto文件中配置接口路由即可 |
Microsoft.AspNetCore.Grpc.Swagger| 使用swagger浏览配置了路由的接口 |


