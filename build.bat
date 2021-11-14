docker build -f CodeIsBug.Admin.Api/Dockerfile -t codeisbugadminapi .
docker run -p 1433:1433 -p 8099:80 codeisbugadminapi
pause