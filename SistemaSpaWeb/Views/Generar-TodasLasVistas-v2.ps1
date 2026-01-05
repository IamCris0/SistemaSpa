# ============================================================================
# Script: Generar-TodasLasVistas.ps1 (v2 - Corregido)
# Descripción: Genera automáticamente todas las vistas faltantes para SistemaSpaWeb
# Autor: Sistema Spa Web
# Fecha: 2026-01-04
# ============================================================================

Write-Host "============================================================" -ForegroundColor Cyan
Write-Host "  GENERADOR AUTOMATICO DE VISTAS - Sistema Spa Web" -ForegroundColor Cyan
Write-Host "============================================================" -ForegroundColor Cyan
Write-Host ""

# Configuración de controladores
$controladores = @{
    "Productos" = @{
        Modelo = "Producto"
        ID = "ProductoID"
        Nombre = "NombreProducto"
        Icono = "box-seam"
        Titulo = "Productos"
        TituloSingular = "Producto"
    }
    "Proveedores" = @{
        Modelo = "Proveedor"
        ID = "ProveedorID"
        Nombre = "NombreProveedor"
        Icono = "truck"
        Titulo = "Proveedores"
        TituloSingular = "Proveedor"
    }
    "Salas" = @{
        Modelo = "Sala"
        ID = "SalaID"
        Nombre = "NombreSala"
        Icono = "door-open"
        Titulo = "Salas"
        TituloSingular = "Sala"
    }
    "Membresias" = @{
        Modelo = "Membresia"
        ID = "MembresiaID"
        Nombre = "NombreMembresia"
        Icono = "award"
        Titulo = "Membresías"
        TituloSingular = "Membresía"
    }
    "Compras" = @{
        Modelo = "Compra"
        ID = "CompraID"
        Nombre = "CompraID"
        Icono = "cart"
        Titulo = "Compras"
        TituloSingular = "Compra"
    }
    "ClientesMembresias" = @{
        Modelo = "ClienteMembresia"
        ID = "ClienteMembresiaID"
        Nombre = "ClienteMembresiaID"
        Icono = "person-badge"
        Titulo = "Clientes Membresías"
        TituloSingular = "Cliente Membresía"
    }
    "DetalleCitas" = @{
        Modelo = "DetalleCita"
        ID = "DetalleCitaID"
        Nombre = "DetalleCitaID"
        Icono = "list-check"
        Titulo = "Detalle de Citas"
        TituloSingular = "Detalle de Cita"
    }
    "DetalleCompras" = @{
        Modelo = "DetalleCompra"
        ID = "DetalleCompraID"
        Nombre = "DetalleCompraID"
        Icono = "cart-plus"
        Titulo = "Detalle de Compras"
        TituloSingular = "Detalle de Compra"
    }
    "DetalleVentas" = @{
        Modelo = "DetalleVenta"
        ID = "DetalleVentaID"
        Nombre = "DetalleVentaID"
        Icono = "bag-plus"
        Titulo = "Detalle de Ventas"
        TituloSingular = "Detalle de Venta"
    }
    "HistorialClientes" = @{
        Modelo = "HistorialCliente"
        ID = "HistorialID"
        Nombre = "HistorialID"
        Icono = "clock-history"
        Titulo = "Historial de Clientes"
        TituloSingular = "Historial"
    }
    "PagosCitas" = @{
        Modelo = "PagoCita"
        ID = "PagoID"
        Nombre = "PagoID"
        Icono = "cash-coin"
        Titulo = "Pagos de Citas"
        TituloSingular = "Pago de Cita"
    }
    "TurnosEmpleados" = @{
        Modelo = "TurnoEmpleado"
        ID = "TurnoID"
        Nombre = "TurnoID"
        Icono = "calendar-week"
        Titulo = "Turnos de Empleados"
        TituloSingular = "Turno"
    }
}

# Función para crear directorio si no existe
function Ensure-Directory {
    param([string]$Path)
    if (-not (Test-Path $Path)) {
        New-Item -ItemType Directory -Path $Path -Force | Out-Null
    }
}

# Función para obtener template de Index
function Get-IndexTemplate {
    param($Config)
    
    $template = @'
@model IEnumerable<SistemaSpaWeb.Models.{0}>

@{{
    ViewData["Title"] = "{1}";
}}

<div class="container mt-4">
    <h1 class="mb-4">
        <i class="bi bi-{2}"></i> Gestión de {1}
    </h1>

    @if (TempData["Success"] != null)
    {{
        <div class="alert alert-success alert-dismissible fade show">
            <i class="bi bi-check-circle"></i> @TempData["Success"]
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </div>
    }}

    @if (TempData["Error"] != null)
    {{
        <div class="alert alert-danger alert-dismissible fade show">
            <i class="bi bi-exclamation-triangle"></i> @TempData["Error"]
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </div>
    }}

    <div class="mb-3">
        <a asp-action="Create" class="btn btn-primary">
            <i class="bi bi-plus-circle"></i> Nuevo/a {3}
        </a>
    </div>

    <div class="card shadow-sm">
        <div class="card-body">
            <div class="table-responsive">
                <table class="table table-hover">
                    <thead class="table-dark">
                        <tr>
                            <th>ID</th>
                            <th>Información</th>
                            <th>Estado</th>
                            <th class="text-center">Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        @foreach (var item in Model)
                        {{
                            <tr>
                                <td>@item.{4}</td>
                                <td>@item.{5}</td>
                                <td>
                                    <span class="badge bg-success">Activo</span>
                                </td>
                                <td class="text-center">
                                    <a asp-action="Details" asp-route-id="@item.{4}"
                                       class="btn btn-sm btn-info" title="Ver Detalles">
                                        <i class="bi bi-eye"></i>
                                    </a>
                                    <a asp-action="Edit" asp-route-id="@item.{4}"
                                       class="btn btn-sm btn-warning" title="Editar">
                                        <i class="bi bi-pencil"></i>
                                    </a>
                                    <a asp-action="Delete" asp-route-id="@item.{4}"
                                       class="btn btn-sm btn-danger" title="Eliminar">
                                        <i class="bi bi-trash"></i>
                                    </a>
                                </td>
                            </tr>
                        }}
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>
'@
    
    return ($template -f $Config.Modelo, $Config.Titulo, $Config.Icono, $Config.TituloSingular, $Config.ID, $Config.Nombre)
}

# Función para obtener template de Create
function Get-CreateTemplate {
    param($Config)
    
    $template = @'
@model SistemaSpaWeb.Models.{0}

@{{
    ViewData["Title"] = "Crear {1}";
}}

<div class="container mt-4">
    <div class="row justify-content-center">
        <div class="col-md-8">
            <div class="card shadow">
                <div class="card-header bg-primary text-white">
                    <h2 class="mb-0">
                        <i class="bi bi-plus-circle"></i> Registrar Nuevo/a {1}
                    </h2>
                </div>
                <div class="card-body">
                    <form asp-action="Create" method="post">
                        <div asp-validation-summary="ModelOnly" class="alert alert-danger"></div>

                        <div class="mb-3">
                            <label asp-for="{2}" class="form-label"></label>
                            <input asp-for="{2}" class="form-control" />
                            <span asp-validation-for="{2}" class="text-danger"></span>
                        </div>

                        <div class="mb-3">
                            <label class="form-label">Información Principal</label>
                            <input class="form-control" placeholder="Ingrese la información" />
                        </div>

                        <div class="mb-3">
                            <label asp-for="Estado" class="form-label">Estado</label>
                            <select asp-for="Estado" class="form-select">
                                <option value="">-- Seleccione --</option>
                                <option value="Activo" selected>Activo</option>
                                <option value="Inactivo">Inactivo</option>
                            </select>
                            <span asp-validation-for="Estado" class="text-danger"></span>
                        </div>

                        <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                            <a asp-action="Index" class="btn btn-secondary">
                                <i class="bi bi-arrow-left"></i> Cancelar
                            </a>
                            <button type="submit" class="btn btn-primary">
                                <i class="bi bi-save"></i> Guardar
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</div>

@section Scripts {{
    @{{
        await Html.RenderPartialAsync("_ValidationScriptsPartial");
    }}
}}
'@
    
    return ($template -f $Config.Modelo, $Config.TituloSingular, $Config.Nombre)
}

# Función para obtener template de Edit
function Get-EditTemplate {
    param($Config)
    
    $template = @'
@model SistemaSpaWeb.Models.{0}

@{{
    ViewData["Title"] = "Editar {1}";
}}

<div class="container mt-4">
    <div class="row justify-content-center">
        <div class="col-md-8">
            <div class="card shadow">
                <div class="card-header bg-warning text-dark">
                    <h2 class="mb-0">
                        <i class="bi bi-pencil"></i> Editar {1}
                    </h2>
                </div>
                <div class="card-body">
                    <form asp-action="Edit" method="post">
                        <div asp-validation-summary="ModelOnly" class="alert alert-danger"></div>
                        <input type="hidden" asp-for="{2}" />

                        <div class="mb-3">
                            <label asp-for="{3}" class="form-label"></label>
                            <input asp-for="{3}" class="form-control" />
                            <span asp-validation-for="{3}" class="text-danger"></span>
                        </div>

                        <div class="mb-3">
                            <label asp-for="Estado" class="form-label">Estado</label>
                            <select asp-for="Estado" class="form-select">
                                <option value="">-- Seleccione --</option>
                                <option value="Activo">Activo</option>
                                <option value="Inactivo">Inactivo</option>
                            </select>
                            <span asp-validation-for="Estado" class="text-danger"></span>
                        </div>

                        <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                            <a asp-action="Index" class="btn btn-secondary">
                                <i class="bi bi-arrow-left"></i> Cancelar
                            </a>
                            <button type="submit" class="btn btn-warning">
                                <i class="bi bi-save"></i> Actualizar
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</div>

@section Scripts {{
    @{{
        await Html.RenderPartialAsync("_ValidationScriptsPartial");
    }}
}}
'@
    
    return ($template -f $Config.Modelo, $Config.TituloSingular, $Config.ID, $Config.Nombre)
}

# Función para obtener template de Details
function Get-DetailsTemplate {
    param($Config)
    
    $template = @'
@model SistemaSpaWeb.Models.{0}

@{{
    ViewData["Title"] = "Detalles de {1}";
}}

<div class="container mt-4">
    <div class="row justify-content-center">
        <div class="col-md-8">
            <div class="card shadow">
                <div class="card-header bg-info text-white">
                    <h2 class="mb-0">
                        <i class="bi bi-info-circle"></i> Detalles de {1}
                    </h2>
                </div>
                <div class="card-body">
                    <dl class="row">
                        <dt class="col-sm-4">ID</dt>
                        <dd class="col-sm-8">@Model.{2}</dd>

                        <dt class="col-sm-4">Información</dt>
                        <dd class="col-sm-8"><strong>@Model.{3}</strong></dd>

                        <dt class="col-sm-4">Estado</dt>
                        <dd class="col-sm-8">
                            <span class="badge bg-success">@Model.Estado</span>
                        </dd>
                    </dl>

                    <hr class="my-4" />

                    <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                        <a asp-action="Index" class="btn btn-secondary">
                            <i class="bi bi-arrow-left"></i> Volver al Listado
                        </a>
                        <a asp-action="Edit" asp-route-id="@Model.{2}" class="btn btn-warning">
                            <i class="bi bi-pencil"></i> Editar
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
'@
    
    return ($template -f $Config.Modelo, $Config.TituloSingular, $Config.ID, $Config.Nombre)
}

# Función para obtener template de Delete
function Get-DeleteTemplate {
    param($Config)
    
    $template = @'
@model SistemaSpaWeb.Models.{0}

@{{
    ViewData["Title"] = "Eliminar {1}";
}}

<div class="container mt-4">
    <div class="row justify-content-center">
        <div class="col-md-8">
            <div class="card shadow border-danger">
                <div class="card-header bg-danger text-white">
                    <h2 class="mb-0">
                        <i class="bi bi-trash"></i> Eliminar {1}
                    </h2>
                </div>
                <div class="card-body">
                    <div class="alert alert-warning">
                        <i class="bi bi-exclamation-triangle"></i>
                        <strong>¿Está seguro de que desea eliminar este/a {1}?</strong>
                    </div>

                    <dl class="row">
                        <dt class="col-sm-4">ID</dt>
                        <dd class="col-sm-8">@Model.{2}</dd>

                        <dt class="col-sm-4">Información</dt>
                        <dd class="col-sm-8"><strong>@Model.{3}</strong></dd>
                    </dl>

                    <hr />

                    <form asp-action="Delete" method="post">
                        <input type="hidden" asp-for="{2}" />
                        <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                            <a asp-action="Index" class="btn btn-secondary">
                                <i class="bi bi-arrow-left"></i> Cancelar
                            </a>
                            <button type="submit" class="btn btn-danger">
                                <i class="bi bi-trash"></i> Eliminar
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</div>
'@
    
    return ($template -f $Config.Modelo, $Config.TituloSingular, $Config.ID, $Config.Nombre)
}

# PROCESO PRINCIPAL
Write-Host "Iniciando generación de vistas..." -ForegroundColor Yellow
Write-Host ""

$totalVistas = 0
$vistasCreadas = 0

foreach ($controlador in $controladores.Keys) {
    Write-Host "Procesando: $controlador" -ForegroundColor Cyan
    
    Ensure-Directory $controlador
    
    $config = $controladores[$controlador]
    
    # Generar Index
    $rutaIndex = Join-Path $controlador "Index.cshtml"
    if (Test-Path $rutaIndex) {
        Write-Host "  ⚠️  Index.cshtml ya existe - omitiendo" -ForegroundColor Yellow
    } else {
        Get-IndexTemplate $config | Out-File -FilePath $rutaIndex -Encoding UTF8
        Write-Host "  ✅ Index.cshtml creado" -ForegroundColor Green
        $vistasCreadas++
    }
    $totalVistas++
    
    # Generar Create
    $rutaCreate = Join-Path $controlador "Create.cshtml"
    if (Test-Path $rutaCreate) {
        Write-Host "  ⚠️  Create.cshtml ya existe - omitiendo" -ForegroundColor Yellow
    } else {
        Get-CreateTemplate $config | Out-File -FilePath $rutaCreate -Encoding UTF8
        Write-Host "  ✅ Create.cshtml creado" -ForegroundColor Green
        $vistasCreadas++
    }
    $totalVistas++
    
    # Generar Edit
    $rutaEdit = Join-Path $controlador "Edit.cshtml"
    if (Test-Path $rutaEdit) {
        Write-Host "  ⚠️  Edit.cshtml ya existe - omitiendo" -ForegroundColor Yellow
    } else {
        Get-EditTemplate $config | Out-File -FilePath $rutaEdit -Encoding UTF8
        Write-Host "  ✅ Edit.cshtml creado" -ForegroundColor Green
        $vistasCreadas++
    }
    $totalVistas++
    
    # Generar Details
    $rutaDetails = Join-Path $controlador "Details.cshtml"
    if (Test-Path $rutaDetails) {
        Write-Host "  ⚠️  Details.cshtml ya existe - omitiendo" -ForegroundColor Yellow
    } else {
        Get-DetailsTemplate $config | Out-File -FilePath $rutaDetails -Encoding UTF8
        Write-Host "  ✅ Details.cshtml creado" -ForegroundColor Green
        $vistasCreadas++
    }
    $totalVistas++
    
    # Generar Delete
    $rutaDelete = Join-Path $controlador "Delete.cshtml"
    if (Test-Path $rutaDelete) {
        Write-Host "  ⚠️  Delete.cshtml ya existe - omitiendo" -ForegroundColor Yellow
    } else {
        Get-DeleteTemplate $config | Out-File -FilePath $rutaDelete -Encoding UTF8
        Write-Host "  ✅ Delete.cshtml creado" -ForegroundColor Green
        $vistasCreadas++
    }
    $totalVistas++
    
    Write-Host ""
}

# GastosOperativos Edit
Write-Host "Procesando: GastosOperativos (Edit)" -ForegroundColor Cyan
Ensure-Directory "GastosOperativos"

$editGastosPath = "GastosOperativos\Edit.cshtml"
if (-not (Test-Path $editGastosPath)) {
    $editGastosContent = @'
@model SistemaSpaWeb.Models.GastoOperativo

@{
    ViewData["Title"] = "Editar Gasto";
}

<div class="container mt-4">
    <div class="row justify-content-center">
        <div class="col-md-8">
            <div class="card shadow">
                <div class="card-header bg-warning text-dark">
                    <h2 class="mb-0">
                        <i class="bi bi-pencil"></i> Editar Gasto Operativo
                    </h2>
                </div>
                <div class="card-body">
                    <form asp-action="Edit" method="post">
                        <div asp-validation-summary="ModelOnly" class="alert alert-danger"></div>
                        <input type="hidden" asp-for="GastoID" />

                        <div class="mb-3">
                            <label asp-for="TipoGasto" class="form-label">Tipo de Gasto</label>
                            <input asp-for="TipoGasto" class="form-control" />
                            <span asp-validation-for="TipoGasto" class="text-danger"></span>
                        </div>

                        <div class="mb-3">
                            <label asp-for="Monto" class="form-label">Monto</label>
                            <input asp-for="Monto" class="form-control" type="number" step="0.01" />
                            <span asp-validation-for="Monto" class="text-danger"></span>
                        </div>

                        <div class="mb-3">
                            <label asp-for="Estado" class="form-label">Estado</label>
                            <select asp-for="Estado" class="form-select">
                                <option value="Pendiente">Pendiente</option>
                                <option value="Pagado">Pagado</option>
                                <option value="Anulado">Anulado</option>
                            </select>
                        </div>

                        <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                            <a asp-action="Index" class="btn btn-secondary">
                                <i class="bi bi-arrow-left"></i> Cancelar
                            </a>
                            <button type="submit" class="btn btn-warning">
                                <i class="bi bi-save"></i> Actualizar
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</div>

@section Scripts {
    @{
        await Html.RenderPartialAsync("_ValidationScriptsPartial");
    }
}
'@
    $editGastosContent | Out-File -FilePath $editGastosPath -Encoding UTF8
    Write-Host "  ✅ Edit.cshtml creado" -ForegroundColor Green
    $vistasCreadas++
    $totalVistas++
}

Write-Host ""
Write-Host "============================================================" -ForegroundColor Green
Write-Host "              GENERACION COMPLETADA" -ForegroundColor Green
Write-Host "============================================================" -ForegroundColor Green
Write-Host ""
Write-Host "Estadísticas:" -ForegroundColor Cyan
Write-Host "   Total de vistas procesadas: $totalVistas" -ForegroundColor White
Write-Host "   Vistas creadas: $vistasCreadas" -ForegroundColor Green
Write-Host "   Vistas omitidas (ya existían): $($totalVistas - $vistasCreadas)" -ForegroundColor Yellow
Write-Host ""
Write-Host "✅ ¡Proceso completado!" -ForegroundColor Green
Write-Host ""
Write-Host "Presiona cualquier tecla para continuar..."
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
