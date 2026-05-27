# Sistema Web PETI Empresarial

Prototipo web basado en el archivo `Formato para Elaborar un Plan estratégico de TI.xlsx`.

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

## Cómo abrirlo

Abra este archivo en el navegador:

`C:\Users\Hashira\Documents\Codex\2026-05-26\files-mentioned-by-the-user-formato\index.html`

No requiere instalar dependencias.

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
