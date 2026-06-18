# Sistema PETI Empresarial

Aplicación web empresarial para crear, gestionar y dar seguimiento a Planes Estratégicos de TI (PETI), alineada al formato Excel del curso.

## Alcance implementado

- Multiempresa simple con aislamiento por organización y persistencia en base de datos.
- Registro de empresa: crea organización y primer usuario con rol `Administrador Empresa`.
- Login normal por correo y contraseña, sin selector público de empresas.
- Gestión de usuarios internos desde el panel del administrador.
- RBAC por rol y área responsable del módulo.
- Asignación de responsables solo por el Administrador Empresa, filtrada por el área del módulo y sin modificar permisos.
- Flujo secuencial obligatorio en todos los módulos PETI implementados.
- Bloqueo de módulos posteriores si el anterior no está completo.
- Dashboard de avance.
- Formularios guiados para información empresarial, misión, visión, valores, objetivos, FODA automático, cadena de valor, BCG, Porter, PEST, estrategia, CAME y resumen ejecutivo.
- Autodiagnóstico completo de cadena de valor con 25 afirmaciones, escala 0-4, validaciones, porcentaje e interpretación.
- Matriz BCG con gráfico en `canvas`, cuadrantes y cálculo de participación relativa.
- Autodiagnóstico PEST con 25 afirmaciones, factores medioambientales, gráfico de barras y factores adicionales para FODA.
- Identificación de estrategia con relaciones `FO`, `AF`, `AD` y `OD`, puntuación calculada automáticamente y ajuste manual opcional.
- Exportación PDF/Excel con tablas y gráficos HTML generados desde los datos registrados.
- Modales para crear, editar, eliminar, asignar responsables, aprobar y ver historial.
- Autoguardado, historial de cambios, usuario responsable, área, fecha y versionado básico.
- Exportación JSON técnica del PETI para administradores.

## Stack

- Frontend: HTML, CSS y JavaScript.
- Backend: Node.js + Express.
- Base de datos: SQLite local o PostgreSQL si existe `DATABASE_URL`.
- Autenticación: token firmado en backend.

## Estructura

```text
frontend/
  index.html
  styles.css
  app.js
  api.js
  assets/

backend/
  server.js
  routes/
  controllers/
  middleware/
  database/

package.json
.env.example
.gitignore
README.md
```

## Instalación local

```bash
git clone https://github.com/Fabri59/Proyecto-ProfeRed.git
cd Proyecto-ProfeRed
npm install
npm run dev
```

Abrir:

```text
http://localhost:3000
```

En la primera ejecución, crear una empresa desde la pantalla de registro. No se crean usuarios ni datos demo automáticamente.

## Variables de entorno

Crear `.env`:

```env
PORT=3000
JWT_SECRET=pon-un-secreto-largo-y-unico
DATABASE_URL=
DB_PATH=./data/peti.sqlite
```

Si `DATABASE_URL` está vacío, la app usa SQLite local. Si `DATABASE_URL` tiene una cadena PostgreSQL, la app usa PostgreSQL.

## Ejecución

Desarrollo:

```bash
npm run dev
```

Producción:

```bash
npm start
```

## Despliegue

1. Configurar variables de entorno en el proveedor:

```env
PORT=3000
JWT_SECRET=un-secreto-largo-y-unico
DATABASE_URL=postgresql://usuario:password@host:5432/peti
```

2. Instalar dependencias y arrancar:

```bash
npm install
npm start
```

3. Usar PostgreSQL para producción. SQLite es recomendable solo para ejecución local.

4. Después del despliegue, registrar la primera empresa desde la pantalla inicial.

### Checklist si el login no funciona en VM

- Verificar que `JWT_SECRET` esté definido y no cambie entre reinicios; si cambia, los tokens guardados dejan de servir.
- Verificar que `DATABASE_URL` apunte a una base PostgreSQL real. No usar el placeholder de `.env.production`.
- Crear la primera empresa desde la pantalla de registro. La app no crea usuarios demo.
- Confirmar que el proxy/reverse proxy envíe `/api/*` al mismo servidor Node que sirve el frontend.
- Revisar logs con `npm start`; errores de conexión a la base impiden login y registro.
- Si se usa SQLite solo para pruebas, asegurar permisos de escritura sobre la carpeta `data/`.


## Roles incluidos

- Administrador Empresa: owner de la empresa, edita todo, aprueba y crea usuarios internos.
- Gerencia: edita módulos estratégicos y aprueba.
- Área TI: edita análisis, objetivos y BCG.
- Administración: edita información empresarial y objetivos.
- Planeamiento: edita Porter y PEST.
- Consultor: edita módulos estratégicos y de análisis.
- Lector: solo lectura.

## Flujo implementado

1. Información de empresa
2. Misión
3. Visión
4. Valores
5. Objetivos estratégicos y UEN
6. Cadena de valor y autodiagnóstico
7. Matriz de Crecimiento - Participación BCG
8. 5 Fuerzas de Porter
9. Análisis PEST
10. Análisis interno y externo FODA
11. Identificación de estrategias
12. Matriz CAME
13. Resumen ejecutivo
