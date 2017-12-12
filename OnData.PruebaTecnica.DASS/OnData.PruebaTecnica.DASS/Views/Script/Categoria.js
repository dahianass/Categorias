angular.module('CategoriasApp', ["kendo.directives"]).
controller('CategoriaCtrl', function ($http) {
    var vm = this;
    vm.listaCategoria = [];
    vm.name = "Hola mundo";
    vm.url = "http://localhost:50069";
    vm.Categorias = ['Diversión', 'Familia', 'Amor', 'Educación'];
    vm.idContenidos;
    vm.selecion;
    vm.CategoriasSelecionadas = [];
    vm.Etiqueta = [];
    vm.TotalCaracteres = 0;
    vm.TipoContenido = "";
    vm.Descripcion = "";
    vm.selectCt = function (Categoria) {
        if (vm.selecion) {
            vm.CategoriasSelecionadas.push(Categoria);
        }
        console.log(vm.CategoriasSelecionadas)
    };
    vm.addImagen = function () {
        debugger;
    }
    vm.validations = function () {
        vm.TotalCaracteres = vm.DescripcionContenido.length;
    }
    vm.AddEtiqueta = function () {
        vm.Etiqueta.push(vm.NombreE);
    }
    vm.eliminar = function (etiqueta) {
        vm.Etiqueta = _.without(vm.Etiqueta, etiqueta);
    }
    function ListarCotenidos() {
        $http({
            method: 'GET',
            url:  vm.url +'/odata/Contenidoes'
        }).then(function successCallback(response) {
            vm.listaCategoria = response.data.value;
            if (vm.optionCallback !== undefined) {
                vm.mainGridOptions.dataSource.transport.read(vm.optionCallback);
            }else{
                CargaLista();
            }
            
        }, function errorCallback(response) {
            debugger;
        });
    }
    vm.detalle = function (IdContenido) {
        informacionmodal(IdContenido);

    }
    function CargaLista() {
        vm.mainGridOptions = {
            dataSource:{
                transport: {
                    dataType: "odata",
                    read: function (e) {
                        e.success(vm.listaCategoria);
                        vm.optionCallback = e;
                    }   
                },
                pageSize: 10,
                height: 200,
                sortable: true,
            },
                pageable: {
                    refresh: true,
                    pageSizes: true,
                    buttonCount: 5
                },

                columns: [
                    { title: "Descripcion", field: "IdContenido", template: "<input type='button' n class='k-button' value='Detalle' ng-click='vm.detalle(#=IdContenido#);' />" },
                    {
                    field: "Nombre",
                    title: "Nombre",
                    headerAttributes: { "ng-non-bindable": true },
                },
                
                {
                    field: "Descripcion",
                    title: "Descripcion",
                }, {
                    field: "TipoContenido",
                }, {
                    field: "Contenido1",
                }]
            //})
        }

    }
    vm.GuardarContenido = function () {
        cont = 0;
        var valida = "";
        if (vm.TipoContenido == "") {
            valida = valida + "Debe selecionar tipo de contenido , ";
            cont++;

        }
        if (vm.CategoriasSelecionadas.length == 0) {
            valida = valida + "Debes selecionar minimo una categoria ";
            cont++;
        }
        if (cont > 0) {
            swal(
       'Formulario incompleto',
       valida,
       'error'
     )
        } else {
            GuardarContenido();
        }
    }
    function GuardarContenido() {
        var catego = [];
        _.each(vm.CategoriasSelecionadas, function (cate) {
            catego.push({IdContenido:0,
                Nombre:cate,})
        });
        var et = [];
        _.each(vm.Etiqueta, function (e) {
            et.push({
                IdContenido: 0,
                Nombre: e,
            })
        });


        var obj = {
            Categorias: catego,
            Nombre: vm.Nombre,
            Descripcion: vm.Descripcion,
            TipoContenido: vm.TipoContenido,
            Contenido1: vm.DescripcionContenido,
            Etiquetas: et,
        }

        var objContenidos = JSON.stringify(obj);
        $http({
            method: 'POST',
            url: vm.url + '/odata/Contenidoes',
            data: objContenidos,
        }).then(function successCallback(response) {
            debugger;
            ListarCotenidos();
            

        }, function errorCallback(response) {
            debugger;
        });
    }

    function informacionmodal(Id) {
        var n =vm.url + '/odata/Categorias?$filter=IdContenido eq ' + Id;
        $http({
            method: 'GET',
            url: n,
        }).then(function successCallback(response) {
            vm.CategoriaXContenido = response.data.value;
            
        }, function errorCallback(response) {
            debugger;
        });
        var nd=vm.url + '/odata/Etiquetas?$filter=IdContenido eq ' + Id;
        $http({
            method: 'GET',
            url:nd
        }).then(function successCallback(response) {
            vm.EtiquetaXContenido = response.data.value;
            debugger;
            $('#myModal').modal();
        }, function errorCallback(response) {
            debugger;
        });


    }

    ListarCotenidos();
    
});