FROM mcr.microsoft.com/mssql-tools

WORKDIR /scripts

COPY . .

RUN chmod +x /scripts/init.sh

ENTRYPOINT ["/scripts/init.sh"]