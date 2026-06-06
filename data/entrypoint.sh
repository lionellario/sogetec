#!/bin/bash
set -e

PORT=${1:-5432} # Use first argument, default to 5432 if not provided

psql -h localhost -p ${PORT} -U postgres -d postgres \
     -f createdb.sql \
     -f script.sql

echo "Database ready"
