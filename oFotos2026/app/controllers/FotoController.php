<?php
// app/controllers/FotoController.php

class FotoController
{
    private $conn;

    public function __construct($db = null)
    {
        // Acepta la conexión que le pasa index.php o la obtiene por su cuenta
        if ($db) {
            $this->conn = $db;
        } else {
            $this->conn = Database::getConnection();
        }
    }

    /**
     * Muestra la vista de captura de foto (webcam / cámara del teléfono).
     * La vista se imprime directamente aquí para no crear archivos extra.
     * Acepta opcionalmente ?cod_empleado=XXXXXX para precargar el código.
     */
    public function mostrarCaptura()
    {
        $cod_empleado = isset($_GET['cod_empleado']) ? $_GET['cod_empleado'] : '';
        $cod_empleado_html = htmlspecialchars($cod_empleado);
?>
        <!DOCTYPE html>
        <html lang="es">

        <head>
            <meta charset="UTF-8">
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
            <title>Captura de Foto - Acutlizacion de Fotos 2026</title>
            <style>
                * {
                    margin: 0;
                    padding: 0;
                    box-sizing: border-box;
                    font-family: 'Segoe UI', Tahoma, sans-serif;
                }

                body {
                    background: linear-gradient(135deg, #464f75 0%, #b08fd0 50%);
                    min-height: 100vh;
                    display: flex;
                    justify-content: center;
                    align-items: center;
                    padding: 20px;
                }

                .container {
                    width: 100%;
                    max-width: 640px;
                    background: #fff;
                    border-radius: 20px;
                    box-shadow: 0 20px 60px rgba(0, 0, 0, .3);
                    overflow: hidden;
                }

                .header {
                    background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                    color: #fff;
                    padding: 22px;
                    text-align: center;
                }

                .header h1 {
                    font-size: 22px;
                }

                .content {
                    padding: 24px;
                }

                .form-group {
                    margin-bottom: 16px;
                }

                label {
                    display: block;
                    font-weight: 600;
                    color: #495057;
                    margin-bottom: 6px;
                    font-size: 14px;
                }

                input[type=text] {
                    width: 100%;
                    padding: 12px 16px;
                    border: 2px solid #e0e0e0;
                    border-radius: 10px;
                    font-size: 16px;
                    text-align: center;
                    font-weight: bold;
                    letter-spacing: 2px;
                }

                input[type=text]:focus {
                    outline: none;
                    border-color: #667eea;
                    box-shadow: 0 0 0 3px rgba(102, 126, 234, .15);
                }

                .video-wrapper {
                    position: relative;
                    background: #000;
                    border-radius: 12px;
                    overflow: hidden;
                    margin: 16px 0;
                    aspect-ratio: 16/9;
                }

                video {
                    width: 100%;
                    height: 100%;
                    object-fit: contain;
                    display: block;
                    background: #000;
                }

                canvas {
                    display: none;
                }

                .preview {
                    width: 100%;
                    border-radius: 12px;
                    margin: 16px 0;
                    display: none;
                    border: 3px solid #667eea;
                }

                .btn {
                    width: 100%;
                    padding: 15px;
                    border: none;
                    border-radius: 10px;
                    font-size: 16px;
                    font-weight: 600;
                    cursor: pointer;
                    transition: all .25s;
                    margin-bottom: 10px;
                }

                .btn:hover {
                    transform: translateY(-2px);
                    box-shadow: 0 10px 22px rgba(0, 0, 0, .15);
                }

                .btn-primary {
                    background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                    color: #fff;
                }

                .btn-success {
                    background: linear-gradient(135deg, #00b894 0%, #00cec9 100%);
                    color: #fff;
                }

                .btn-warn {
                    background: linear-gradient(135deg, #fdcb6e 0%, #e17055 100%);
                    color: #fff;
                }

                .btn-secondary {
                    background: #e9ecef;
                    color: #495057;
                }

                .btn:disabled {
                    opacity: .5;
                    cursor: not-allowed;
                    transform: none;
                }

                .row {
                    display: flex;
                    gap: 10px;
                }

                .row .btn {
                    flex: 1;
                }

                .estado {
                    padding: 12px;
                    border-radius: 10px;
                    text-align: center;
                    font-weight: 600;
                    margin-bottom: 14px;
                    font-size: 14px;
                }

                .estado-ok {
                    background: #d4edda;
                    color: #155724;
                }

                .estado-err {
                    background: #f8d7da;
                    color: #721c24;
                }

                .estado-info {
                    background: #d1ecf1;
                    color: #0c5460;
                }

                .switch-cam {
                    background: #f8f9fa;
                    border: 2px dashed #dee2e6;
                    border-radius: 10px;
                    padding: 10px;
                    text-align: center;
                    font-size: 13px;
                    color: #6c757d;
                    margin-bottom: 10px;
                }
            </style>
        </head>

        <body>
            <div class="container">
                <div class="header">
                    <h1>📸 Captura de Foto - Acutlizacion de Fotos 2026</h1>
                </div>
                <div class="content">

                    <div id="estado" class="estado estado-info">Inicializando cámara...</div>

                    <div class="form-group">
                        <label>Código de Empleado</label>
                        <input type="text" id="cod_empleado" placeholder="Ej: 123456"
                            value="<?php echo $cod_empleado_html; ?>"
                            autocomplete="off" maxlength="20">
                    </div>

                    <!-- <div class="switch-cam">
                        ¿No ves imagen?
                        <button type="button" class="btn btn-secondary" style="width:auto;padding:6px 14px;margin:0"
                            onclick="cambiarCamara()">🔄 Cambiar cámara</button>
                    </div> -->

                    <div class="video-wrapper">
                        <video id="video" autoplay playsinline muted></video>
                    </div>
                    <canvas id="canvas"></canvas>
                    <img id="preview" class="preview" alt="Vista previa">

                    <div class="row" id="accionesCamara">
                        <button class="btn btn-primary" onclick="tomarFoto()">📷 Tomar Foto</button>
                    </div>

                    <div class="row" id="accionesPreview" style="display:none">
                        <button class="btn btn-warn" onclick="repetirFoto()">🔁 Repetir</button>
                        <button class="btn btn-success" onclick="subirFoto()">☁️ Guardar Foto</button>
                    </div>

                </div>
            </div>

            <script>
                let stream = null;
                let camaraActual = 0;
                let camarasDisponibles = [];
                let fotoBase64 = null;

                const video = document.getElementById('video');
                const canvas = document.getElementById('canvas');
                const preview = document.getElementById('preview');
                const estadoEl = document.getElementById('estado');

                function setEstado(txt, tipo) {
                    estadoEl.textContent = txt;
                    estadoEl.className = 'estado estado-' + (tipo || 'info');
                }

                async function listarCamaras() {
                    try {
                        const devices = await navigator.mediaDevices.enumerateDevices();
                        camarasDisponibles = devices.filter(d => d.kind === 'videoinput');
                    } catch (e) {
                        camarasDisponibles = [];
                    }
                }

                async function iniciarCamara(indice) {
                    if (stream) {
                        stream.getTracks().forEach(t => t.stop());
                    }

                    // Pedir Full HD como objetivo principal
                    const constraintsBase = {
                        video: {
                            width: {
                                ideal: 1920
                            },
                            height: {
                                ideal: 1080
                            },
                            facingMode: indice === 1 ? 'environment' : 'user'
                        },
                        audio: false
                    };

                    try {
                        stream = await navigator.mediaDevices.getUserMedia(constraintsBase);
                        video.srcObject = stream;

                        video.onloadedmetadata = function() {
                            const track = stream.getVideoTracks()[0];
                            const settings = track.getSettings();

                            const w = settings.width || video.videoWidth;
                            const h = settings.height || video.videoHeight;

                            // Mostrar en pantalla la resolución REAL entregada
                            setEstado(
                                '✅ Cámara activa (' + w + '×' + h + '). Presiona "Tomar Foto".',
                                'ok'
                            );
                            console.log('Resolución real del stream:', w, 'x', h);
                            console.log('Settings completos:', settings);
                        };
                    } catch (err) {
                        setEstado('❌ No se pudo acceder a la cámara: ' + err.message, 'err');
                        console.error(err);
                    }
                }

                async function cambiarCamara() {
                    await listarCamaras();
                    if (camarasDisponibles.length <= 1) {
                        setEstado('Solo hay una cámara disponible.', 'info');
                        return;
                    }
                    camaraActual = (camaraActual + 1) % camarasDisponibles.length;
                    const deviceId = camarasDisponibles[camaraActual].deviceId;
                    if (stream) stream.getTracks().forEach(t => t.stop());
                    try {
                        stream = await navigator.mediaDevices.getUserMedia({
                            video: {
                                deviceId: {
                                    exact: deviceId
                                },
                                width: {
                                    ideal: 1280
                                },
                                height: {
                                    ideal: 720
                                }
                            },
                            audio: false
                        });
                        video.srcObject = stream;
                        setEstado('Cámara cambiada.', 'ok');
                    } catch (e) {
                        setEstado('No se pudo cambiar de cámara: ' + e.message, 'err');
                    }
                }

                function tomarFoto() {
                    // Usar la resolución REAL del stream (no la del CSS)
                    const w = video.videoWidth || 1280;
                    const h = video.videoHeight || 720;

                    canvas.width = w;
                    canvas.height = h;

                    const ctx = canvas.getContext('2d');
                    // Mejor calidad de interpolación al dibujar
                    ctx.imageSmoothingEnabled = true;
                    ctx.imageSmoothingQuality = 'high';
                    ctx.drawImage(video, 0, 0, w, h);

                    // Calidad 1.0 = máxima calidad JPEG (sin pérdida adicional visible)
                    fotoBase64 = canvas.toDataURL('image/jpeg', 1.0);

                    preview.src = fotoBase64;
                    preview.style.display = 'block';
                    video.style.display = 'none';

                    document.getElementById('accionesCamara').style.display = 'none';
                    document.getElementById('accionesPreview').style.display = 'flex';
                    setEstado('📸 Foto capturada a ' + w + '×' + h + '. Puedes repetirla o guardarla.', 'info');
                }

                function repetirFoto() {
                    fotoBase64 = null;
                    preview.style.display = 'none';
                    video.style.display = 'block';
                    document.getElementById('accionesCamara').style.display = 'flex';
                    document.getElementById('accionesPreview').style.display = 'none';
                    setEstado('✅ Cámara activa. Presiona "Tomar Foto".', 'ok');
                }

                async function subirFoto() {
                    const cod = document.getElementById('cod_empleado').value.trim();
                    if (!cod) {
                        alert('Ingresa el código del empleado');
                        return;
                    }
                    if (!fotoBase64) {
                        alert('Primero toma una foto');
                        return;
                    }

                    setEstado('⏳ Subiendo foto...', 'info');

                    const fd = new FormData();
                    fd.append('cod_empleado', cod);
                    fd.append('imagen_base64', fotoBase64);

                    try {
                        const resp = await fetch('index.php?action=guardarFoto', {
                            method: 'POST',
                            body: fd
                        });
                        const json = await resp.json();
                        if (json.ok) {
                            setEstado('✅ Foto guardada: ' + json.archivo, 'ok');
                            setTimeout(() => {
                                if (confirm('¿Tomar foto de otro empleado?')) {
                                    document.getElementById('cod_empleado').value = '';
                                    repetirFoto();
                                }
                            }, 600);
                        } else {
                            setEstado('❌ Error: ' + json.error, 'err');
                        }
                    } catch (e) {
                        setEstado('❌ Error de red: ' + e.message, 'err');
                    }
                }

                window.addEventListener('load', async () => {
                    await listarCamaras();
                    await iniciarCamara(0);
                });
            </script>
        </body>

        </html>
<?php
    }

    /**
     * Recibe la imagen por POST (base64) y la guarda en la carpeta fotos/
     * como {COD_EMPLEADO}.jpg
     */
    public function guardarFoto()
    {
        header('Content-Type: application/json');

        try {
            $cod_empleado = $_POST['cod_empleado'] ?? '';
            $cod_empleado = $this->extraerUltimos6Digitos($cod_empleado);

            if (empty($cod_empleado)) {
                echo json_encode(['ok' => false, 'error' => 'Código de empleado vacío']);
                return;
            }

            // Validar que el empleado exista en la BD
            $query = "SELECT COD_EMPLEADO FROM RRHH.EMPLEADOS WHERE COD_EMPLEADO = :cod";
            $stid = oci_parse($this->conn, $query);
            oci_bind_by_name($stid, ':cod', $cod_empleado);
            oci_execute($stid);
            $existe = oci_fetch_assoc($stid);
            oci_free_statement($stid);

            if (!$existe) {
                echo json_encode(['ok' => false, 'error' => 'Empleado no existe en BD']);
                return;
            }

            if (!isset($_POST['imagen_base64']) || empty($_POST['imagen_base64'])) {
                echo json_encode(['ok' => false, 'error' => 'No se recibió imagen']);
                return;
            }

            $dataUrl = $_POST['imagen_base64'];

            if (!preg_match('/^data:image\/(\w+);base64,/', $dataUrl, $m)) {
                echo json_encode(['ok' => false, 'error' => 'Formato de imagen inválido']);
                return;
            }

            $extension = strtolower($m[1]);
            if (!in_array($extension, ['jpeg', 'jpg', 'png', 'webp'])) {
                echo json_encode(['ok' => false, 'error' => 'Extensión no permitida']);
                return;
            }

            $binario = base64_decode(substr($dataUrl, strpos($dataUrl, ',') + 1));
            if ($binario === false) {
                echo json_encode(['ok' => false, 'error' => 'Error decodificando base64']);
                return;
            }

            // Guardar SIEMPRE como .jpg en la carpeta fotos/
            $carpeta = __DIR__ . '/../../fotos/';
            if (!is_dir($carpeta)) {
                mkdir($carpeta, 0775, true);
            }

            $nombreArchivo = $cod_empleado . '.jpg';
            $rutaDestino   = $carpeta . $nombreArchivo;

            // Borrar fotos previas con otras extensiones
            $this->borrarFotosPrevias($cod_empleado, $carpeta);

            if (file_put_contents($rutaDestino, $binario) === false) {
                echo json_encode(['ok' => false, 'error' => 'No se pudo escribir la imagen']);
                return;
            }

            echo json_encode([
                'ok'      => true,
                'archivo' => 'fotos/' . $nombreArchivo,
                'codigo'  => $cod_empleado,
                'bytes'   => strlen($binario)
            ]);
        } catch (Exception $e) {
            error_log("Error guardando foto: " . $e->getMessage());
            echo json_encode(['ok' => false, 'error' => $e->getMessage()]);
        }
    }

    private function borrarFotosPrevias($cod_empleado, $carpeta)
    {
        $exts = ['.JPG', '.jpg', '.JPEG', '.jpeg', '.PNG', '.png', '.WEBP', '.webp'];
        foreach ($exts as $ext) {
            $f = $carpeta . $cod_empleado . $ext;
            if (file_exists($f)) {
                @unlink($f);
            }
        }
    }

    private function extraerUltimos6Digitos($codigo)
    {
        $codigo_limpio = preg_replace('/[^0-9]/', '', $codigo);
        if (strlen($codigo_limpio) > 6) {
            return substr($codigo_limpio, -6);
        }
        return $codigo_limpio;
    }
}
