# Sistema PETI Empresarial

Aplicación web empresarial para crear, gestionar y dar seguimiento a Planes Estratégicos de TI (PETI).  
Ahora incluye frontend, backend, autenticación real y persistencia en SQLite.

## Alcance implementado

- Multiempresa simple con aislamiento por organización y datos persistidos en `localStorage`.
- Registro de empresa: crea organización y primer usuario con rol `Administrador Empresa`.
- Login normal por correo y contraseña, sin selector público de empresas.
- Gestión de usuarios internos desde el panel del administrador.
- RBAC por rol y área responsable del módulo.
- Asignación de responsables solo por el Administrador Empresa, filtrada por el área del módulo y sin modificar permisos.
- Flujo secuencial obligatorio en todos los módulos PETI implementados.
- Bloqueo de módulos posteriores si el anterior no está completo.
- Dashboard de avance.
- Formularios guiados para información empresarial, misión, visión, valores, objetivos, FODA, cadena de valor, BCG, Porter, PEST, estrategia, CAME y resumen ejecutivo.
- Autodiagnóstico completo de cadena de valor con 25 afirmaciones, escala 0-4, validaciones, porcentaje e interpretación.
- Matriz BCG con gráfico en `canvas`, cuadrantes y cálculo de participación relativa.
- Modales para crear, editar, eliminar, asignar responsables, aprobar y ver historial.
- Autoguardado, historial de cambios, usuario responsable, área, fecha y versionado básico.
- Exportación JSON del PETI.

## Stack

- Frontend: HTML, CSS y JavaScript.
- Backend: Node.js + Express.
- Base de datos: SQLite.
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
cp .env.example .env
npm run dev
```

Abrir:

```text
http://localhost:3000
```

Demo inicial creada automáticamente:

```text
Correo: owner@nova.pe
Contraseña: demo123
```

## Enlace de sistema desplegado

http://38.250.116.71:3001/


## Ejecución

Desarrollo:

```bash
npm run dev
```

Producción:

```bash
npm start
```


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
6. Análisis interno y externo
7. Cadena de valor y autodiagnóstico
8. Matriz de Crecimiento - Participación BCG
9. 5 Fuerzas de Porter
10. Análisis PEST
11. Identificación de estrategias
12. Matriz CAME
13. Resumen ejecutivo
