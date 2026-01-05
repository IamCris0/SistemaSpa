# ============================================================================
# Script: Generar-TodasLasVistas.ps1
# Descripción: Genera automáticamente todas las vistas faltantes para SistemaSpaWeb
# Autor: Sistema Spa Web
# Fecha: 2026-01-04
# Uso: Ejecutar desde: PS C:\Users\gcris\Desktop\SistemaSpa\SistemaSpaWeb\Views>
# ============================================================================

Write-Host "╔════════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
Write-Host "║   GENERADOR AUTOMÁTICO DE VISTAS - Sistema Spa Web        ║" -ForegroundColor Cyan
Write-Host "╚════════════════════════════════════════════════════════════╝" -ForegroundColor Cyan
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
        Campos = @("NombreProducto", "Descripcion", "Marca", "PrecioUnitario", "Stock", "StockMinimo", "Estado")
    }
    "Proveedores" = @{
        Modelo = "Proveedor"
        ID = "ProveedorID"
        Nombre = "NombreProveedor"
        Icono = "truck"
        Titulo = "Proveedores"
        TituloSingular = "Proveedor"
        Campos = @("NombreProveedor", "Contacto", "Telefono", "Email", "Direccion", "Estado")
    }
    "Salas" = @{
        Modelo = "Sala"
        ID = "SalaID"
        Nombre = "NombreSala"
        Icono = "door-open"
        Titulo = "Salas"
        TituloSingular = "Sala"
        Campos = @("NombreSala", "TipoSala", "Capacidad", "Estado")
    }
    "Membresias" = @{
        Modelo = "Membresia"
        ID = "MembresiaID"
        Nombre = "NombreMembresia"
        Icono = "award"
        Titulo = "Membresías"
        TituloSingular = "Membresía"
        Campos = @("NombreMembresia", "Descripcion", "DuracionMeses", "Precio", "Descuento", "Estado")
    }
    "Compras" = @{
        Modelo = "Compra"
        ID = "CompraID"
        Nombre = "CompraID"
        Icono = "cart"
        Titulo = "Compras"
        TituloSingular = "Compra"
        Campos = @("ProveedorID", "FechaCompra", "Total", "EstadoCompra", "Observaciones")
    }
    "ClientesMembresias" = @{
        Modelo = "ClienteMembresia"
        ID = "ClienteMembresiaID"
        Nombre = "ClienteMembresiaID"
        Icono = "person-badge"
        Titulo = "Clientes Membresías"
        TituloSingular = "Cliente Membresía"
        Campos = @("ClienteID", "MembresiaID", "FechaInicio", "FechaFin", "EstadoMembresia")
    }
    "DetalleCitas" = @{
        Modelo = "DetalleCita"
        ID = "DetalleCitaID"
        Nombre = "DetalleCitaID"
        Icono = "list-check"
        Titulo = "Detalle de Citas"
        TituloSingular = "Detalle de Cita"
        Campos = @("CitaID", "ServicioID", "PrecioServicio", "Descuento")
    }
    "DetalleCompras" = @{
        Modelo = "DetalleCompra"
        ID = "DetalleCompraID"
        Nombre = "DetalleCompraID"
        Icono = "cart-plus"
        Titulo = "Detalle de Compras"
        TituloSingular = "Detalle de Compra"
        Campos = @("CompraID", "ProductoID", "Cantidad", "PrecioUnitario")
    }
    "DetalleVentas" = @{
        Modelo = "DetalleVenta"
        ID = "DetalleVentaID"
        Nombre = "DetalleVentaID"
        Icono = "bag-plus"
        Titulo = "Detalle de Ventas"
        TituloSingular = "Detalle de Venta"
        Campos = @("VentaID", "ProductoID", "Cantidad", "PrecioUnitario")
    }
    "HistorialClientes" = @{
        Modelo = "HistorialCliente"
        ID = "HistorialID"
        Nombre = "HistorialID"
        Icono = "clock-history"
        Titulo = "Historial de Clientes"
        TituloSingular = "Historial"
        Campos = @("ClienteID", "CitaID", "FechaVisita", "Observaciones", "Calificacion")
    }
    "PagosCitas" = @{
        Modelo = "PagoCita"
        ID = "PagoID"
        Nombre = "PagoID"
        Icono = "cash-coin"
        Titulo = "Pagos de Citas"
        TituloSingular = "Pago de Cita"
        Campos = @("CitaID", "FechaPago", "Monto", "MetodoPago", "EstadoPago")
    }
    "TurnosEmpleados" = @{
        Modelo = "TurnoEmpleado"
        ID = "TurnoID"
        Nombre = "TurnoID"
        Icono = "calendar-week"
        Titulo = "Turnos de Empleados"
        TituloSingular = "Turno"
        Campos = @("EmpleadoID", "DiaSemana", "HoraInicio", "HoraFin", "TipoTurno", "Estado")
    }
}

# Función para crear directorio si no existe
function Ensure-Directory {
    param([string]$Path)
    if (-not (Test-Path $Path)) {
        New-Item -ItemType Directory -Path $Path -Force | Out-Null
    }
}

# Función para generar vista Index
function Generate-IndexView {
    param($Config, $Controlador)
    
    $content = @"
@model IEnumerable<SistemaSpaWeb.Models.$($Config.Modelo)>

@{
    ViewData["Title"] = "$($Config.Titulo)";
}

<div class="container mt-4">
    <h1 class="mb-4">
        <i class="bi bi-$($Config.Icono)"></i> Gestión de $($Config.Titulo)
    </h1>

    @if (TempData["Success"] != null)
    {
        <div class="alert alert-success alert-dismissible fade show">
            <i class="bi bi-check-circle"></i> @TempData["Success"]
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </div>
    }

    @if (TempData["Error"] != null)
    {
        <div class="alert alert-danger alert-dismissible fade show">
            <i class="bi bi-exclamation-triangle"></i> @TempData["Error"]
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </div>
    }

    <div class="mb-3">
        <a asp-action="Create" class="btn btn-primary">
            <i class="bi bi-plus-circle"></i> Nuevo/a $($Config.TituloSingular)
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
                        {
                            <tr>
                                <td>@item.$($Config.ID)</td>
                                <td>
                                    @* Mostrar información relevante del modelo *@
                                    @item.$($Config.Nombre)
                                </td>
                                <td>
                                    @if (item.Estado != null)
                                    {
                                        <span class=`"badge @(item.Estado == "Activo" ? "bg-success" : "bg-secondary")`">
                                            @item.Estado
                                        </span>
                                    }
                                </td>
                                <td class="text-center">
                                    <a asp-action="Details" asp-route-id="@item.$($Config.ID)"
                                       class="btn btn-sm btn-info" title="Ver Detalles">
                                        <i class="bi bi-eye"></i>
                                    </a>
                                    <a asp-action="Edit" asp-route-id="@item.$($Config.ID)"
                                       class="btn btn-sm btn-warning" title="Editar">
                                        <i class="bi bi-pencil"></i>
                                    </a>
                                    <a asp-action="Delete" asp-route-id="@item.$($Config.ID)"
                                       class="btn btn-sm btn-danger" title="Eliminar">
                                        <i class="bi bi-trash"></i>
                                    </a>
                                </td>
                            </tr>
                        }
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>
"@
    return $content
}

# Función para generar vista Create
function Generate-CreateView {
    param($Config, $Controlador)
    
    $content = @"
@model SistemaSpaWeb.Models.$($Config.Modelo)

@{
    ViewData["Title"] = "Crear $($Config.TituloSingular)";
}

<div class="container mt-4">
    <div class="row justify-content-center">
        <div class="col-md-8">
            <div class="card shadow">
                <div class="card-header bg-primary text-white">
                    <h2 class="mb-0">
                        <i class="bi bi-plus-circle"></i> Registrar Nuevo/a $($Config.TituloSingular)
                    </h2>
                </div>
                <div class="card-body">
                    <form asp-action="Create" method="post">
                        <div asp-validation-summary="ModelOnly" class="alert alert-danger"></div>

                        @* Campos del formulario *@
"@

    # Agregar campos dinámicamente
    foreach ($campo in $Config.Campos) {
        if ($campo -like "*ID" -and $campo -ne $Config.ID) {
            # Es una FK - crear un select
            $content += @"

                        <div class="mb-3">
                            <label asp-for="$campo" class="form-label"></label>
                            <select asp-for="$campo" class="form-select" asp-items="ViewBag.$campo">
                                <option value="">-- Seleccione --</option>
                            </select>
                            <span asp-validation-for="$campo" class="text-danger"></span>
                        </div>
"@
        } elseif ($campo -eq "Estado") {
            $content += @"

                        <div class="mb-3">
                            <label asp-for="Estado" class="form-label"></label>
                            <select asp-for="Estado" class="form-select">
                                <option value="">-- Seleccione --</option>
                                <option value="Activo" selected>Activo</option>
                                <option value="Inactivo">Inactivo</option>
                            </select>
                            <span asp-validation-for="Estado" class="text-danger"></span>
                        </div>
"@
        } elseif ($campo -like "*Fecha*") {
            $content += @"

                        <div class="mb-3">
                            <label asp-for="$campo" class="form-label"></label>
                            <input asp-for="$campo" class="form-control" type="date" />
                            <span asp-validation-for="$campo" class="text-danger"></span>
                        </div>
"@
        } elseif ($campo -like "*Precio*" -or $campo -like "*Monto*" -or $campo -like "*Total*") {
            $content += @"

                        <div class="mb-3">
                            <label asp-for="$campo" class="form-label"></label>
                            <input asp-for="$campo" class="form-control" type="number" step="0.01" min="0" />
                            <span asp-validation-for="$campo" class="text-danger"></span>
                        </div>
"@
        } elseif ($campo -like "*Descripcion*" -or $campo -like "*Observaciones*") {
            $content += @"

                        <div class="mb-3">
                            <label asp-for="$campo" class="form-label"></label>
                            <textarea asp-for="$campo" class="form-control" rows="3"></textarea>
                            <span asp-validation-for="$campo" class="text-danger"></span>
                        </div>
"@
        } else {
            $content += @"

                        <div class="mb-3">
                            <label asp-for="$campo" class="form-label"></label>
                            <input asp-for="$campo" class="form-control" />
                            <span asp-validation-for="$campo" class="text-danger"></span>
                        </div>
"@
        }
    }

    $content += @"

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

@section Scripts {
    @{
        await Html.RenderPartialAsync("_ValidationScriptsPartial");
    }
}
"@
    return $content
}

# Función para generar vista Edit
function Generate-EditView {
    param($Config, $Controlador)
    
    $content = @"
@model SistemaSpaWeb.Models.$($Config.Modelo)

@{
    ViewData["Title"] = "Editar $($Config.TituloSingular)";
}

<div class="container mt-4">
    <div class="row justify-content-center">
        <div class="col-md-8">
            <div class="card shadow">
                <div class="card-header bg-warning text-dark">
                    <h2 class="mb-0">
                        <i class="bi bi-pencil"></i> Editar $($Config.TituloSingular)
                    </h2>
                </div>
                <div class="card-body">
                    <form asp-action="Edit" method="post">
                        <div asp-validation-summary="ModelOnly" class="alert alert-danger"></div>
                        <input type="hidden" asp-for="$($Config.ID)" />

"@

    # Agregar campos dinámicamente (igual que Create)
    foreach ($campo in $Config.Campos) {
        if ($campo -like "*ID" -and $campo -ne $Config.ID) {
            $content += @"
                        <div class="mb-3">
                            <label asp-for="$campo" class="form-label"></label>
                            <select asp-for="$campo" class="form-select" asp-items="ViewBag.$campo">
                                <option value="">-- Seleccione --</option>
                            </select>
                            <span asp-validation-for="$campo" class="text-danger"></span>
                        </div>
"@
        } elseif ($campo -eq "Estado") {
            $content += @"
                        <div class="mb-3">
                            <label asp-for="Estado" class="form-label"></label>
                            <select asp-for="Estado" class="form-select">
                                <option value="">-- Seleccione --</option>
                                <option value="Activo">Activo</option>
                                <option value="Inactivo">Inactivo</option>
                            </select>
                            <span asp-validation-for="Estado" class="text-danger"></span>
                        </div>
"@
        } elseif ($campo -like "*Fecha*") {
            $content += @"
                        <div class="mb-3">
                            <label asp-for="$campo" class="form-label"></label>
                            <input asp-for="$campo" class="form-control" type="date" />
                            <span asp-validation-for="$campo" class="text-danger"></span>
                        </div>
"@
        } elseif ($campo -like "*Descripcion*" -or $campo -like "*Observaciones*") {
            $content += @"
                        <div class="mb-3">
                            <label asp-for="$campo" class="form-label"></label>
                            <textarea asp-for="$campo" class="form-control" rows="3"></textarea>
                            <span asp-validation-for="$campo" class="text-danger"></span>
                        </div>
"@
        } else {
            $content += @"
                        <div class="mb-3">
                            <label asp-for="$campo" class="form-label"></label>
                            <input asp-for="$campo" class="form-control" />
                            <span asp-validation-for="$campo" class="text-danger"></span>
                        </div>
"@
        }
    }

    $content += @"

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
"@
    return $content
}

# Función para generar vista Details
function Generate-DetailsView {
    param($Config, $Controlador)
    
    $content = @"
@model SistemaSpaWeb.Models.$($Config.Modelo)

@{
    ViewData["Title"] = "Detalles de $($Config.TituloSingular)";
}

<div class="container mt-4">
    <div class="row justify-content-center">
        <div class="col-md-8">
            <div class="card shadow">
                <div class="card-header bg-info text-white">
                    <h2 class="mb-0">
                        <i class="bi bi-info-circle"></i> Detalles de $($Config.TituloSingular)
                    </h2>
                </div>
                <div class="card-body">
                    <dl class="row">
                        <dt class="col-sm-4">ID</dt>
                        <dd class="col-sm-8">@Model.$($Config.ID)</dd>
"@

    # Agregar campos
    foreach ($campo in $Config.Campos) {
        $content += @"

                        <dt class="col-sm-4">$campo</dt>
                        <dd class="col-sm-8">@Model.$campo</dd>
"@
    }

    $content += @"

                    </dl>

                    <hr class="my-4" />

                    <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                        <a asp-action="Index" class="btn btn-secondary">
                            <i class="bi bi-arrow-left"></i> Volver al Listado
                        </a>
                        <a asp-action="Edit" asp-route-id="@Model.$($Config.ID)" class="btn btn-warning">
                            <i class="bi bi-pencil"></i> Editar
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
"@
    return $content
}

# Función para generar vista Delete
function Generate-DeleteView {
    param($Config, $Controlador)
    
    $content = @"
@model SistemaSpaWeb.Models.$($Config.Modelo)

@{
    ViewData["Title"] = "Eliminar $($Config.TituloSingular)";
}

<div class="container mt-4">
    <div class="row justify-content-center">
        <div class="col-md-8">
            <div class="card shadow border-danger">
                <div class="card-header bg-danger text-white">
                    <h2 class="mb-0">
                        <i class="bi bi-trash"></i> Eliminar $($Config.TituloSingular)
                    </h2>
                </div>
                <div class="card-body">
                    <div class="alert alert-warning">
                        <i class="bi bi-exclamation-triangle"></i>
                        <strong>¿Está seguro de que desea eliminar este/a $($Config.TituloSingular)?</strong>
                    </div>

                    <dl class="row">
                        <dt class="col-sm-4">ID</dt>
                        <dd class="col-sm-8">@Model.$($Config.ID)</dd>
"@

    foreach ($campo in $Config.Campos) {
        $content += @"

                        <dt class="col-sm-4">$campo</dt>
                        <dd class="col-sm-8">@Model.$campo</dd>
"@
    }

    $content += @"

                    </dl>

                    <hr />

                    <form asp-action="Delete" method="post">
                        <input type="hidden" asp-for="$($Config.ID)" />
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
"@
    return $content
}

# PROCESO PRINCIPAL
Write-Host "Iniciando generación de vistas..." -ForegroundColor Yellow
Write-Host ""

$totalVistas = 0
$vistasCreadas = 0

foreach ($controlador in $controladores.Keys) {
    Write-Host "Procesando: $controlador" -ForegroundColor Cyan
    
    # Crear directorio del controlador
    Ensure-Directory $controlador
    
    $config = $controladores[$controlador]
    
    # Generar cada vista
    $vistas = @{
        "Index.cshtml" = { Generate-IndexView $config $controlador }
        "Create.cshtml" = { Generate-CreateView $config $controlador }
        "Edit.cshtml" = { Generate-EditView $config $controlador }
        "Details.cshtml" = { Generate-DetailsView $config $controlador }
        "Delete.cshtml" = { Generate-DeleteView $config $controlador }
    }
    
    foreach ($vista in $vistas.Keys) {
        $rutaCompleta = Join-Path $controlador $vista
        $totalVistas++
        
        # Verificar si ya existe
        if (Test-Path $rutaCompleta) {
            Write-Host "  ⚠️  $vista ya existe - omitiendo" -ForegroundColor Yellow
        } else {
            $contenido = & $vistas[$vista]
            Set-Content -Path $rutaCompleta -Value $contenido -Encoding UTF8
            Write-Host "  ✅ $vista creado" -ForegroundColor Green
            $vistasCreadas++
        }
    }
    
    Write-Host ""
}

# Vista especial para GastosOperativos/Edit.cshtml
Write-Host "Procesando: GastosOperativos (Edit)" -ForegroundColor Cyan
Ensure-Directory "GastosOperativos"

$editGastosPath = "GastosOperativos\Edit.cshtml"
if (-not (Test-Path $editGastosPath)) {
    $editGastosContent = @"
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
                        <input type="hidden" asp-for="FechaRegistro" />

                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label asp-for="ProveedorID" class="form-label">Proveedor</label>
                                <select asp-for="ProveedorID" class="form-select" asp-items="ViewBag.ProveedorID">
                                    <option value="">-- Seleccione --</option>
                                </select>
                                <span asp-validation-for="ProveedorID" class="text-danger"></span>
                            </div>
                            <div class="col-md-6 mb-3">
                                <label asp-for="TipoGasto" class="form-label">Tipo de Gasto</label>
                                <input asp-for="TipoGasto" class="form-control" />
                                <span asp-validation-for="TipoGasto" class="text-danger"></span>
                            </div>
                        </div>

                        <div class="mb-3">
                            <label asp-for="Descripcion" class="form-label">Descripción</label>
                            <textarea asp-for="Descripcion" class="form-control" rows="3"></textarea>
                            <span asp-validation-for="Descripcion" class="text-danger"></span>
                        </div>

                        <div class="row">
                            <div class="col-md-4 mb-3">
                                <label asp-for="Monto" class="form-label">Monto</label>
                                <input asp-for="Monto" class="form-control" type="number" step="0.01" />
                                <span asp-validation-for="Monto" class="text-danger"></span>
                            </div>
                            <div class="col-md-4 mb-3">
                                <label asp-for="FechaGasto" class="form-label">Fecha</label>
                                <input asp-for="FechaGasto" class="form-control" type="datetime-local" />
                                <span asp-validation-for="FechaGasto" class="text-danger"></span>
                            </div>
                            <div class="col-md-4 mb-3">
                                <label asp-for="MetodoPago" class="form-label">Método Pago</label>
                                <select asp-for="MetodoPago" class="form-select">
                                    <option value="Efectivo">Efectivo</option>
                                    <option value="Tarjeta">Tarjeta</option>
                                    <option value="Transferencia">Transferencia</option>
                                    <option value="Cheque">Cheque</option>
                                </select>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label asp-for="Comprobante" class="form-label">Comprobante</label>
                                <input asp-for="Comprobante" class="form-control" />
                            </div>
                            <div class="col-md-6 mb-3">
                                <label asp-for="Estado" class="form-label">Estado</label>
                                <select asp-for="Estado" class="form-select">
                                    <option value="Pendiente">Pendiente</option>
                                    <option value="Pagado">Pagado</option>
                                    <option value="Anulado">Anulado</option>
                                </select>
                            </div>
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
"@
    Set-Content -Path $editGastosPath -Value $editGastosContent -Encoding UTF8
    Write-Host "  ✅ Edit.cshtml creado" -ForegroundColor Green
    $vistasCreadas++
    $totalVistas++
}

Write-Host ""
Write-Host "╔════════════════════════════════════════════════════════════╗" -ForegroundColor Green
Write-Host "║                    GENERACIÓN COMPLETADA                   ║" -ForegroundColor Green
Write-Host "╚════════════════════════════════════════════════════════════╝" -ForegroundColor Green
Write-Host ""
Write-Host "📊 Estadísticas:" -ForegroundColor Cyan
Write-Host "   Total de vistas procesadas: $totalVistas" -ForegroundColor White
Write-Host "   Vistas creadas: $vistasCreadas" -ForegroundColor Green
Write-Host "   Vistas omitidas (ya existían): $($totalVistas - $vistasCreadas)" -ForegroundColor Yellow
Write-Host ""
Write-Host "✅ ¡Todas las vistas han sido generadas exitosamente!" -ForegroundColor Green
Write-Host ""
Write-Host "📝 Nota: Revisa cada vista y ajusta según tus necesidades específicas." -ForegroundColor Yellow
Write-Host ""
