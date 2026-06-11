#!/bin/sh

echo "Waiting for SQL Server..."

until /opt/mssql-tools/bin/sqlcmd \
  -S sqlserver \
  -U sa \
  -P "$SA_PASSWORD" \
  -Q "SELECT 1" > /dev/null 2>&1
do
  echo "SQL Server not ready..."
  sleep 5
done

echo "SQL Server ready."

/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P "$SA_PASSWORD" -i /scripts/01-create-database.sql

/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P "$SA_PASSWORD" -d UserManagementDB -i /scripts/02-create-schema.sql

/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P "$SA_PASSWORD" -d UserManagementDB -i /scripts/03-create-tables.sql

/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P "$SA_PASSWORD" -d UserManagementDB -i /scripts/04-seed-data.sql

echo "Database initialization completed."