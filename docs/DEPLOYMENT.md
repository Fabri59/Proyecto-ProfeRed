# Instrucciones de Despliegue

## 🌍 Despliegue en Producción

### Requisitos

- Servidor Windows Server 2016+
- SQL Server 2019+
- IIS 10+
- .NET 8.0 Hosting Bundle
- Node.js (solo para build del frontend)

### 1. Preparación

#### Backend

```bash
# En carpeta del proyecto backend
dotnet publish -c Release -o ./publish

# Los archivos en ./publish/ se suben al servidor
```

#### Frontend

```bash
# En carpeta del proyecto frontend
npm run build

# Los archivos en ./dist/ se suben al servidor
```

### 2. Configurar Base de Datos

En el servidor SQL Server de producción:

```sql
-- Ejecutar los scripts de creación
-- database/01_create_database.sql
-- database/02_stored_procedures.sql

-- Crear usuario SQL Server para la aplicación
CREATE LOGIN [planestrategi_user] WITH PASSWORD='PasswordFuerteAqui123!';
CREATE USER [planestrategi_user] FOR LOGIN [planestrategi_user];
ALTER ROLE [db_owner] ADD MEMBER [planestrategi_user];
```

### 3. Configurar IIS

#### Application Pool

1. Abrir IIS Manager
2. Crear nuevo Application Pool: "PlanEstrategico"
3. Configurar:
   - .NET CLR Version: No Managed Code
   - Managed Pipeline Mode: Integrated
   - Start Automatically: ✓

#### Website

1. Agregar nuevo Website:
   - Name: "PlanEstrategico"
   - Physical Path: `C:\inetpub\planestrategi o\api`
   - Application Pool: "PlanEstrategico"
   - Binding: `http://*:80` o `https://*:443`

2. Para HTTPS (recomendado):
   - Instalar certificado SSL
   - Configurar binding HTTPS

### 4. Configurar el Backend

Editar `appsettings.Production.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=SERVIDOR_SQL;Database=PlanEstrategicopTI;User Id=planestrategi_user;Password=PasswordFuerteAqui123!;"
  },
  "JwtSettings": {
    "SecretKey": "GENERAR_CLAVE_LARGA_Y_UNICA_PARA_PRODUCCION_MINIMO_32_CARACTERES",
    "Issuer": "PlanEstrategico",
    "Audience": "PlanEstrategico",
    "ExpirationMinutes": 480
  },
  "Cors": {
    "AllowedOrigins": [
      "https://tudominio.com",
      "https://www.tudominio.com"
    ]
  }
}
```

### 5. Desplegar Frontend

1. Crear nuevo Website en IIS para el frontend
2. Physical Path: `C:\inetpub\planestrategi o\web`
3. Copiar archivos de `dist/` a esa carpeta

4. Crear archivo `web.config`:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <system.webServer>
    <rewrite>
      <rules>
        <rule name="React Router Rule" stopProcessing="true">
          <match url=".*" />
          <conditions logicalGrouping="MatchList">
            <add input="{REQUEST_FILENAME}" matchType="IsFile" negate="true" />
            <add input="{REQUEST_FILENAME}" matchType="IsDirectory" negate="true" />
          </conditions>
          <action type="Rewrite" url="/" />
        </rule>
      </rules>
    </rewrite>
  </system.webServer>
</configuration>
```

### 6. Configurar HTTPS

```powershell
# Generar certificado autofirmado (desarrollo)
New-SelfSignedCertificate -DnsName "planestrategi o.local" -CertStoreLocation "cert:\LocalMachine\My"

# En producción, usar certificado de autoridad
# Let's Encrypt, Comodo, DigiCert, etc.
```

### 7. Configurar DNS y Dominio

1. Apuntar registro A del dominio a IP del servidor
2. Apuntar registro CNAME para www (opcional)
3. Esperar propagación (24-48 horas)

### 8. Monitoreo

#### Logs en Windows Event Viewer

```powershell
# Ver errores de aplicación
Get-EventLog -LogName Application -Source ".NET Runtime" -Newest 50
```

#### SQL Server Logs

```sql
-- Verificar crecimiento de BD
EXEC sp_helpdb 'PlanEstrategicopTI'

-- Ver active connections
SELECT * FROM sys.dm_exec_sessions WHERE database_id > 4
```

#### Configurar backups automáticos

```sql
-- Backup completo diario
DECLARE @backupPath NVARCHAR(256) = N'D:\Backups\PlanEstrategicopTI_' + 
  CONVERT(NVARCHAR, GETDATE(), 112) + '_' + 
  CONVERT(NVARCHAR, GETDATE(), 108) + N'.bak'

BACKUP DATABASE PlanEstrategicopTI 
  TO DISK = @backupPath 
  WITH COMPRESSION
```

## 🔒 Seguridad Recomendada

- [ ] HTTPS obligatorio
- [ ] Firewall configurado
- [ ] SQL Server en puerto no estándar
- [ ] Contraseña fuerte en BD
- [ ] JWT SecretKey única y segura
- [ ] CORS limitado a dominios específicos
- [ ] Rate limiting en API
- [ ] WAF (Web Application Firewall)
- [ ] Backups regulares
- [ ] Logs centralizados

## 🚀 Escalabilidad

### Load Balancing

```
Usuarios
    ↓
[Load Balancer]
    ↓
[Server 1] [Server 2] [Server 3]
    ↓
[SQL Server Cluster]
```

### Caching

```csharp
// Agregar Redis
services.AddStackExchangeRedisCache(options => {
    options.Configuration = "servidor-redis:6379";
});
```

### CDN para Frontend

- Cloudflare
- AWS CloudFront
- Azure CDN

## 📊 Monitoreo y Alertas

### Application Insights (Azure)

```csharp
services.AddApplicationInsightsTelemetry();
```

### DataDog

```json
{
  "datadog": {
    "apiKey": "tu-api-key",
    "site": "datadoghq.com"
  }
}
```

## 🆘 Recuperación ante Desastres

### Plan de Backup

```powershell
# Backup automático cada 6 horas
$schedule = New-ScheduledTaskTrigger -At 12:00am -RepetitionInterval (New-TimeSpan -Hours 6) -RepetitionDuration ([timespan]::MaxValue)

Register-ScheduledTask -Action $action -Trigger $schedule -TaskName "BackupPlanEstrategi o"
```

### Restore

```sql
-- Restaurar BD desde backup
RESTORE DATABASE PlanEstrategicopTI 
  FROM DISK = N'D:\Backups\PlanEstrategicopTI_backup.bak'
  WITH REPLACE
```

---

**¡Sistema listo para producción! 🎉**
