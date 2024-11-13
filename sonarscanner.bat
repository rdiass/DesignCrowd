dotnet sonarscanner begin /k:"DesignCrowd" /d:sonar.host.url="http://localhost:9000"  /d:sonar.token="sqp_ecd503be53896ebedb75a9f0540f223f2444781c" /d:sonar.cs.vscoveragexml.reportsPaths=coverage.xml
dotnet build --no-incremental
dotnet-coverage collect 'dotnet test' -f xml  -o 'coverage.xml'
dotnet sonarscanner end /d:sonar.token="sqp_ecd503be53896ebedb75a9f0540f223f2444781c"