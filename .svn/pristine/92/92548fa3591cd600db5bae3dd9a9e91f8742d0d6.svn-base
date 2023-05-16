$(document).ready(function () {
    var folder_name = $("#folderID").children("option:selected").text();

    var input_key = $("#exampleInputKey");
    var input_En = $("#exampleInputEn");
    var input_Km = $("#exampleInputKm");
    var input_Th = $("#exampleInputTh");
    var input_Vi = $("#exampleInputVi");
    var input_Zh = $("#exampleInputZh");
    var input_My = $("#exampleInputMy");

    var valid_key = $("#validationLabelKey");
    var valid_En = $("#validationLabelEn");
    var valid_Km = $("#validationLabelKm");
    var valid_Th = $("#validationLabelTh");
    var valid_Vi = $("#validationLabelVi");
    var valid_Zh = $("#validationLabelZh");
    var valid_My = $("#validationLabelMy");



    $("#folderID").change(function () {
        $("#input_search").val("");
        this.form.submit();
    })
    function CheckWhiteSpace(input, label, text) {
        var value = $(input).val();
        var res = /^\S+$/.test(value);
        if (!res) {
            $(label).text(text);
            $(label).show();
            return false;
        }
        else {
            $(label).hide();
        }
        return true;
    }
    function CheckEmpty(input, label, text) {
        var value = $(input).val();
        if (value.length === 0) {
            $(label).text(text);
            $(label).show();
            return false;
        }
        else {
            $(label).hide();
        }
        return true;
    }
    
    function HideLabel() {
        valid_key.hide();
        valid_En.hide();
        valid_Km.hide();
        valid_Th.hide();
        valid_Vi.hide();
        valid_Zh.hide();
        valid_My.hide();
    }
    function Validation() {
        var check_en = CheckEmpty(input_En, valid_En, "Vui lòng nhập dữ liệu");
        var check_km = CheckEmpty(input_Km, valid_Km, "Vui lòng nhập dữ liệu");
        var check_th = CheckEmpty(input_Th, valid_Th, "Vui lòng nhập dữ liệu");
        var check_vi = CheckEmpty(input_Vi, valid_Vi, "Vui lòng nhập dữ liệu");
        var check_zh = CheckEmpty(input_Zh, valid_Zh, "Vui lòng nhập dữ liệu");
        var check_My = CheckEmpty(input_My, valid_My, "Vui lòng nhập dữ liệu");
        var check_key_white_space = CheckWhiteSpace(input_key, "#validationLabelKey", "Vui lòng dữ liệu không được rỗng và có khoảng trắng");

        return check_key_white_space && check_en && check_km && check_th && check_vi && check_zh && check_My;
    }
    // Function Add
    // Ajax Update
    function AddLang(folder_name, model) {
        return $.ajax({
            url: "/Home/Add",
            data: {
                folder_name: folder_name,
                model: model
            },
            dataType: "json",
            type: "POST",
        })
    }

    $("#btn_addModal").on("click", function () {
        HideLabel();
        input_key.val("");
        input_En.val("");
        input_Km.val("");
        input_Th.val("");
        input_Vi.val("");
        input_Zh.val("");
        input_My.val("");
        $(".btnUpdate").hide();
        $(".btnAdd").show();
        $("#exampleModalLabel").text("Thêm Dữ Liệu Vào " + folder_name);
        input_key.prop('disabled', false);
    });
    $(".btnAdd").on("click", function () {

        var value_key = input_key.val();
        var value_en = input_En.val();
        var value_km = input_Km.val();
        var value_th = input_Th.val();
        var value_vi = input_Vi.val();
        var value_zh = input_Zh.val();
        var value_My = input_My.val();

        
        var model = {
            Key: value_key,
            En: value_en,
            Km: value_km,
            Th: value_th,
            Vi: value_vi,
            Zh: value_zh,
            My: value_My
        }
        var res = Validation();
        if (res) {


            AddLang(folder_name, model)
                .done(function (kq) {
                    if (kq.status == 1) {

                        Swal.fire({
                            icon: 'success',
                            title: 'Bạn đã thêm thành công !.',
                            showConfirmButton: true,
                            timer: 1000
                        }).then((result) => {
                            /* Phần code */
                            location.reload();
                            /* End Phần code */
                        })
                    }
                    
                    else if (kq.status == -1) {
                        Swal.fire({
                            icon: 'error',
                            title: 'Dữ liệu không được trống !.',
                            showConfirmButton: true,
                            timer: 1000
                        }).then((result) => {
                            /* Phần code */
                            
                            /* End Phần code */
                        })
                    }
                    else if (kq.status == -2){
                        Swal.fire({
                            icon: 'error',
                            title: 'Key đã tồn tại !',
                            html: 'Vui lòng kiểm tra lại !',
                            showConfirmButton: true,
                            timer: 1000
                        }).then((result) => {
                            /* Phần code */

                            /* End Phần code */
                        })
                    }
                })
                .fail(function (kq) {
                    
                })
        }
    });


    // Function Update
    // Ajax Update
    function UpdateLang(folder_name, model) {
        return $.ajax({
            url: "/Home/Update",
            data: {
                folder_name: folder_name,
                model: model
            },
            dataType: "json",
            type: "POST",
        })
    }

    

    $(".btn_updateModal").on("click", function (e) {
        e.preventDefault();
        HideLabel();
        var key = $(this).data("key");
        var row = ".key_" + key;
        var value_key = $(row + " " + ".table-key").text();
        var value_en = $(row + " " + ".table-en").text();
        var value_km = $(row + " " + ".table-km").text();
        var value_th = $(row + " " + ".table-th").text();
        var value_vi = $(row + " " + ".table-vi").text();
        var value_zh = $(row + " " + ".table-zh").text();
        var value_My = $(row + " " + ".table-my").text();

        input_key.val(value_key);
        input_En.val(value_en);
        input_Km.val(value_km);
        input_Th.val(value_th);
        input_Vi.val(value_vi);
        input_Zh.val(value_zh);
        input_My.val(value_My);

        $(".btnUpdate").show();
        $(".btnAdd").hide();

        $("#exampleModalLabel").text("Sửa Dữ Liệu " + folder_name);
        input_key.prop('disabled', true);

        
    });

    $(".btnUpdate").on("click", function () {

        var value_key = input_key.val();
        var value_en = input_En.val();
        var value_km = input_Km.val();
        var value_th = input_Th.val();
        var value_vi = input_Vi.val();
        var value_zh = input_Zh.val();
        var value_My = input_My.val();

        var model = {
            Key: value_key,
            En: value_en,
            Km: value_km,
            Th: value_th,
            Vi: value_vi,
            Zh: value_zh,
            My: value_My
        }
       

        var res = Validation();

        if (res) {

            Swal.fire({
                title: '<div>Bạn chắc chắn có muốn cập nhật dữ liệu <span class="text-danger">' + folder_name + '</span> Tại Key <span class="text-danger">' + value_key + '</span></div>',
                icon: 'warning',
                showCancelButton: true,
                cancelButtonColor: '#d33',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Xác nhận!',
                cancelButtonText: 'Đóng',
            }).then((result) => {
                if (result.value) {


                    /* Phần code */
                    UpdateLang(folder_name, model)
                        .done(function (kq) {
                            if (kq.status == 1) {

                                Swal.fire({
                                    icon: 'success',
                                    title: 'Bạn đã cập nhật thành công !.',
                                    showConfirmButton: true,
                                    timer: 1000
                                }).then((result) => {
                                    /* Phần code */
                                    location.reload();
                                    /* End Phần code */
                                })

                            }
                            else if (kq.status == -1) {
                                Swal.fire({
                                    icon: 'warning',
                                    title: 'Dữ liệu không được trống !.',
                                    showConfirmButton: true,
                                    timer: 1000
                                }).then((result) => {
                                    /* Phần code */
                                    
                                    /* End Phần code */
                                })
                            }
                        })
                        .fail(function (kq) {
                           
                        })
                    /*End Phần code */
                }
            })
        }
    });



    // Function Delete
    // Ajax Delete
    function DeleteLang(folder_name, key_name) {
        return $.ajax({
            url: "/Home/Delete",
            data: {
                folder_name: folder_name,
                key_name: key_name
            },
            dataType: "json",
            type: "POST",
        })
    }
    // Sự kiện nút delete
    $(".btn_deleteModal").on("click", function (e) {
        e.preventDefault();
        /* Phần code */
        var key_name = $(this).data("name");
        
        
        /*End Phần code */

        Swal.fire({
            title: '<div>Xóa dữ liệu ở thư mục <span class="text-danger">' + folder_name + '</span> ?</div>',
            html: '<div>Bạn có chắc chắn muốn xóa dữ liệu KEY <p class="text-danger m-0"> ' + key_name + '</p></div>',
            icon: 'warning',
            showCancelButton: true,
            cancelButtonColor: '#d33',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Xác nhận!',
            cancelButtonText: 'Đóng',
        }).then((result) => {
            if (result.value) {

                
                /* Phần code */
                DeleteLang(folder_name, key_name)
                    .done(function (kq) {
                        if (kq) {

                            Swal.fire({
                                icon: 'success',
                                title: 'Bạn đã xóa thành công !.',
                                showConfirmButton: true,
                                timer: 1000
                            }).then((result) => {
                                /* Phần code */
                                location.reload();
                                /* End Phần code */
                            })
                        }
                        else {
                            
                        }
                    })
                    .fail(function (kq) {
                       
                    })
                /*End Phần code */



                
            }
        })

        
    });
});