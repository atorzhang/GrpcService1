
#引入镜像，低版本 docker 去掉  AS base
#换成别人做的阿里镜像
#FROM registry.cn-hangzhou.aliyuncs.com/newbe36524
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

#配置工作目录 相当于cd
WORKDIR /app
  
#暴露容器端口，此端口与程序运行路径一致，可
EXPOSE 5000

#复制文件到工作目录
COPY . .

#对图片的支持
RUN apt-get update -y && apt-get install -y libgdiplus && apt-get clean && ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll

#ENV ：配置系统环境变量，比如程序环境环境等在这里配置（开发、预发、线上环境）
#这里是配置程序运行端口，如果程序不使用默认的80端口这里一定要设置（程序运行端口）
ENV ASPNETCORE_URLS http://+:5000

#设置时间为中国上海，默认为UTC时间
ENV TZ=Asia/Shanghai
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

#启动程序
ENTRYPOINT ["dotnet", "FJRH.RTM.Service.dll"]