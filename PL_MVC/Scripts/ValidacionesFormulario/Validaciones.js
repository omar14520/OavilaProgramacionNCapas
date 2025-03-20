console.log("hola")
    function setError(input, message) {
        input.style.borderColor = 'red';
    var Errorspan = input.parentNode.querySelector('.error');
    if (Errorspan) {
        Errorspan.textContent = message;

        }
    input.classList.add('error-input');
    }
    function LimpiarError(input) {
        input.style.borderColor = 'green';
    var Errorspan = input.parentNode.querySelector('.error');
    if (Errorspan) {
        Errorspan.textContent = '';
        }
    input.classList.remove('error-input');
    }

    $('form').on('submit', function (evt) {
        if ($(this).find('.error-input').length > 0) {
        evt.preventDefault();
    alert('Porfavor Complete La Informacion Correctamente');
        }
    });
    function ValContra(evt) {
        var inputField = evt.target;
    var ErrorMessage = inputField.parentNode.querySelector('.error');
    ErrorMessage.textContent = '';
    var password = inputField.value;

    var passwordRegex = /^(?=.*[A-Z])(?=.*[\W_]).{8}$/; // 1 mayúscula, 1 carácter especial, exactamente 8 caracteres

    if (!passwordRegex.test(password)) {
        evt.preventDefault();
    setError(inputField, 'Error');
    ErrorMessage.textContent = 'La contraseña debe tener 8 caracteres, una mayúscula y un carácter especial.';
        } else {
        console.log("Contraseña válida");
    LimpiarError(inputField);
        }
    }

    function confirmarContrasena(evt) {
        var confirmField = evt.target;
    var passwordField = document.getElementById('password'); // Asegúrate de que el input de contraseña tenga este ID
    var ErrorMessage = confirmField.parentNode.querySelector('.error');
    ErrorMessage.textContent = '';

    if (confirmField.value !== passwordField.value) {
        evt.preventDefault();
    setError(inputField, '.Error');
    ErrorMessage.textContent = 'Las contraseñas no coinciden.';
    alert('Las contraseñas no son iguales');
        } else {
        console.log("Las contraseñas coinciden");
    LimpiarError(inputField);
        }
    }
    function ValidacionCorreo(evt) {
        var inputField = evt.target;
    var ErrorMessage = inputField.parentNode.querySelector('.error');
    ErrorMessage.textContent = '';

    var email = inputField.value;
    var emailRegex = /^[^\s@@]+@@[^\s@@]+\.[^\s@@]+$/;

    if (!emailRegex.test(email)) {
        evt.preventDefault();
    setError(inputField,'.error')
    ErrorMessage.textContent = 'Ingrese un correo válido';
        } else {
        console.log("Correo válido");
    setError(inputField);
        }
    }
    function validarInput(input) {
        var curp = input.value.toUpperCase(),
    resultado = document.getElementById("resultado"),
    valido = "No válido";

    if (curpValida(curp)) {
        valido = "Válido";
    resultado.classList.add("ok");
        } else {
        resultado.classList.remove("ok");
        }

    resultado.innerText = "CURP: " + curp + "\nFormato: " + valido;
    }

    function curpValida(curp) {
        var re = /^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0\d|1[0-2])(?:[0-2]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$/,
    validado = curp.match(re);

    if (!validado)  //Coincide con el formato general?
    return false;

    //Validar que coincida el dígito verificador
    function digitoVerificador(curp17) {
            //Fuente https://consultas.curp.gob.mx/CurpSP/
            var diccionario = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ",
    lngSuma = 0.0,
    lngDigito = 0.0;
    for (var i = 0; i < 17; i++)
    lngSuma = lngSuma + diccionario.indexOf(curp17.charAt(i)) * (18 - i);
    lngDigito = 10 - lngSuma % 10;
    if (lngDigito == 10)
    return 0;
    return lngDigito;
        }
    if (validado[2] != digitoVerificador(validado[1]))
    return false;

    return true; //Validado
    }
    function SoloNumeros(evt) {
        var entrada = String.fromCharCode(evt.which);
    var inputField = evt.target;
    var ErrorMessage = inputField.parentNode.querySelector('.error');

    ErrorMessage.textContent = '';
    if (!/[0-9]/.test(entrada)) {
        evt.preventDefault();
    setError(inputField, '.error');
    ErrorMessage.textContent = 'Solo se aceptan números';
        }
    else {
        console.log("Si es un Número");
    LimpiarError(inputField);
        }
    }
    function SoloLetras(evt) {
        var entrada = String.fromCharCode(evt.which)
    var inputField = evt.target;
    var ErrorMessage = inputField.parentNode.querySelector('.error')
    ErrorMessage.textContent = '';
    if (!(/[a-z A-Z]/.test(entrada))) {
        evt.preventDefault()
            setError(inputField,'Solo se aceptan letras')
            //ErrorMessage.textContent = 'Solo se aceptan letras';
        }
    else {
        console.log("si es una letra")
            LimpiarError(inputField);
        }
    }

    $("#datepicker").datepicker({
        dateFormat: "dd/mm/yy",
    showAnim:"drop"
          });


    function MunicipioGetByIdEstado() {
        let ddl = $('#ddlEstado').val();
    $.ajax({
        url: ddlMunicipio + ddl,
    type: "GET",
    dataType: "JSON",
    success: function (result) {
                if (result.Correct) {
        let ddlMunicipio = $('#ddlMunicipio');
    ddlMunicipio.empty();
    let municipioDefault = "<option value=0>Selecciona el Municipio</option>";
ddlMunicipio.append(municipioDefault);
$.each(result.Objects, function (i, valor) {
    let option = "<option value=" + valor.IdMunicipio + ">" + valor.Nombre + "</option>";
    ddlMunicipio.append(option);
})
                }
            },
error: function (xhr) {
    console.log(xhr)
}

        })
    }
function ColoniaGetByIdMunicipio() {
    let ddl = $('#ddlMunicipio').val();
    $.ajax({
        url: ddlColonia + ddl,
        type: "GET",
        dataType: "JSON",
        success: function (result) {
            if (result.Correct) {
                let ddlColonia = $('#ddlColonia');
                $.each(result.Objects, function (i, valor) {
                    let option = "<option value=" + valor.IdColonia + ">" + valor.Nombre + "</option>";
                    ddlColonia.append(option)
                })
            }
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}
function validarImagen() {
    var input = $('#inptFileImagen')[0].files[0].name.split('.').pop().toLowerCase()
    var extensionesValidas = ['png', 'jpg', 'jpeg', 'webp']
    var banderaImg = false;

    for (var i = 0; i <= extensionesValidas.length; i++) {
        if (input == extensionesValidas[i]) {
            banderaImg = true
        }
    }
    if (!banderaImg) {
        alert(`Las extenciones permitidas son: ${extensionesValidas}`)
        $('#inptFileImagen').val("");
    }

}
function visualizarImagen(input) {
    if (input.files) {
        var reader = new FileReader();
        reader.onload = function (elemento) {
            $('#img').attr('src', elemento.target.result)
        }
        reader.readAsDataURL(input.files[0])
    }
}