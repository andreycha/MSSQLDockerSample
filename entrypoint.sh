#!/bin/bash
set -e

if [ "$1" = '/opt/mssql/bin/sqlservr' ]; then
  # Initialize the database on container's first run
  if [ ! -f /tmp/database-initialized ]; then
    function init_database() {
      # Wait for SQL Server to start before initializing the databse
      /wait-for-it.sh localhost:1433 --timeout=120

      echo 'Applying scripts...'
      
      /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Password_123 -Q "CREATE DATABASE Test"
      /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Password_123 -d Test -i /tabRecord.sql -I

      echo 'Scripts has been applied'

      # Mark container as initialized
      touch /tmp/database-initialized

      echo 'Database has been initialized'
    }
    init_database &
  else
	  echo 'Database is already initialized'
  fi
fi

exec "$@"