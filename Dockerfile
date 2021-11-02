FROM mcr.microsoft.com/mssql/server:2019-latest

COPY ./wait-for-it.sh /
COPY ./Dockerfile /
COPY ./entrypoint.sh /
COPY ./tabRecord.sql /

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=Password_123

USER root
RUN chmod +x /wait-for-it.sh

ENTRYPOINT [ "/bin/bash", "entrypoint.sh", "/opt/mssql/bin/sqlservr" ]