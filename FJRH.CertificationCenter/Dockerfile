
#���뾵�񣬵Ͱ汾 docker ȥ��  AS base
#���ɱ������İ��ﾵ��
#FROM registry.cn-hangzhou.aliyuncs.com/newbe36524
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

#���ù���Ŀ¼ �൱��cd
WORKDIR /app
  
#��¶�����˿ڣ��˶˿����������·��һ�£���
EXPOSE 6000

#�����ļ�������Ŀ¼
COPY . .
 
#ENV ������ϵͳ����������������򻷾����������������ã�������Ԥ�������ϻ�����
#���������ó������ж˿ڣ��������ʹ��Ĭ�ϵ�80�˿�����һ��Ҫ���ã��������ж˿ڣ�
ENV ASPNETCORE_URLS http://+:6000

#����ʱ��Ϊ�й��Ϻ���Ĭ��ΪUTCʱ��
ENV TZ=Asia/Shanghai
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

#��������
ENTRYPOINT ["dotnet", "FJRH.CertificationCenter.dll"]