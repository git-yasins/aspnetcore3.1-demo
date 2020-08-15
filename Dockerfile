FROM microsoft/dotnet:sdk AS build-env

WORKDIR /code

COPY *.csproj /code

RUN dotnet restore

COPY . /code
RUN dotnet publish -c Release -o out

FROM  microsoft/dotnet:runtime

COPY --from=build-env /code/out /app

#执行
CMD [ "dotnet","aspnetcore3-demo.dll" ]

