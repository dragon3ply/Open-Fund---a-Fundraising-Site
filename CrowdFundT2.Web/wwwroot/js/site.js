// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/* Generic JS (All pages)*/

let successAlert = $('.js-success-alert').hide();
let dangerAlert = $('.js-danger-alert').hide();

/* Create effect in fixed buttons */

$(document).ready(function () {
    let effect = $('#effect-container').hide();
    let flag = false;

    $('#create-container').on('click', () => {

        if (flag) {
            effect.fadeOut(800);
            flag = false;
        } else {
            effect.fadeIn(800).css('display', 'flex');
            flag = true;
        }

    });


});

/* Search General in Index page */

let searchGeneral = $('#search-general').on('click', () => {
    let inputGeneral = $('#search-general-input');
    window.location.assign('/Project?Title=' + inputGeneral.val() + '&Description=' + inputGeneral.val());
});

searchGeneral = $('#search-general-input').on('keypress', (e) => {
    let inputGeneral = $('#search-general-input');

    if (e.keyCode == 13) {
        window.location.assign('/Project?Title=' + inputGeneral.val() + '&Description=' + inputGeneral.val());
    }
});

/* Index invest */

let investInd = $('.js-index-car-invest');
let investPid = $('.js-ProjectId').val();

investInd.on('click', () => {
    window.location.assign('/Project/Details/' + investPid);
});

/* Angels in Index*/

let angelspopup = $('.js-angels-pop-up');
angelspopup.hide();

flag = false;

$(window).click(function (e) {
    //Hide the menus if visible onclick out of element
    if (e.target.className == 'js-angels-pop-up') {
        angelspopup.hide();
        flag = false;
    }
});

$('.js-angels-x-btn').on('click', () => {
    if (!flag) {
        angelspopup.show();
        flag = true;
    } else {
        angelspopup.hide();
        flag = false;
    }
});

$('.js-angels-btn').on('click', () => {
    if (!flag) {
        angelspopup.show();
        flag = true;
    } else {
        flag = false;
    }
});


/* CLIENTS */

/* Create Client */

let btn = $('#js-submit-client-create').on('click', () => {

    let firstname = $('.js-firstname').val();
    let lastname = $('.js-lastname').val();
    let email = $('.js-email').val();
    let phone = $('.js-phone').val();
    

    let data = {
        firstname: firstname,
        lastname: lastname,
        email: email,
        phone: phone
    }

    $.ajax({
        type: 'Post',
        url: '/Client/Create/',
        contentType: 'application/json',
        data: JSON.stringify(data)
    }).done(client => {
        successAlert.css('margin-top', '10px');
        successAlert.css('text-align', 'center');
        successAlert.show().delay(2000);
        successAlert.fadeOut();
        setTimeout(function () {
            window.location.assign("/Client/");
        }, 3000);
    }).fail(failureResponse => {
        dangerAlert.css('margin-top', '10px');
        dangerAlert.css('text-align', 'center');
        dangerAlert.html(failureResponse.responseText);
        dangerAlert.show().delay(2000);
        dangerAlert.fadeOut();
    });

});


/* Create Project */

btn = $('#js-submit-project-create')
    .on('click', () => {

    let clientId = parseInt($('.js-ClientId').val());
    let title = $('.js-Title').val();
    let description = $('.js-Description').val();
    let photos = $('.js-Photos').val();
    let videos = $('.js-Videos').val();
    let postStatusUpdates = $('.js-PostStatusUpdates').val();
    let category = parseInt($('.js-Category').val());
    let projectCost = parseInt($('.js-ProjectCost').val());
    let numberOfPackages = parseInt($('.js-numberOfPackages').val());

    let data = {
        clientId: clientId,
        title: title,
        description: description,
        photos: photos,
        videos: videos,
        postStatusUpdates: postStatusUpdates,
        category: category,
        projectCost: projectCost
    }

    $.ajax({
        type: 'Post',
        url: '/Project/Create?numberOfPackages=' + numberOfPackages,
        contentType: 'application/json',
        data: JSON.stringify(data)
    }).done(project => {
        $('.form-control').removeClass('shadow bg-white rounded');
        successAlert.css('margin-top', '10px');
        successAlert.css('text-align', 'center');
        successAlert.show().delay(2000);
        successAlert.fadeOut();
        
        if (project.result == 'Redirect')
            window.location = project.url;

    }).fail(failureResponse => {
        $('.input-validation-error').addClass('shadow bg-white rounded');
        dangerAlert.css('margin-top', '10px');
        dangerAlert.css('text-align', 'center');
        dangerAlert.html(failureResponse.responseText);
        dangerAlert.show().delay(2000);
        dangerAlert.fadeOut();
    });
        
    });

/*Create Package*/

btn = $('.js-submit-package-create').on('click', () => {
    let numRequest = parseInt($('.js-numberOfPackages').val());
    let projectId = parseInt($('.js-projectId').val());


    for (var i = 1; i <= numRequest; i++) {

        let description = $('.descriptionpack_' + i).val();
        let reward = parseInt($('.rewardpack_' + i).val());

        let data = {
            description: description,
            reward: reward,
            projectId: projectId
        }
        debugger;
        $.ajax({
            type: 'Post',
            url: '/Package/Create',
            contentType: 'application/json',
            data: JSON.stringify(data)
        }).done(project => {
            debugger;
            $('.form-control').removeClass('shadow bg-white rounded');
            successAlert.css('margin-top', '10px');
            successAlert.css('text-align', 'center');
            successAlert.show().delay(2000);
            successAlert.fadeOut();
            setTimeout(function () {
                window.location.assign("/Project/Details/" + projectId);
            }, 3000);
            debugger;
        }).fail(failureResponse => {
            debugger;
            $('.input-validation-error').addClass('shadow bg-white rounded');
            successAlert.hide();
            dangerAlert.css('margin-top', '10px');
            dangerAlert.css('text-align', 'center');
            dangerAlert.html(failureResponse.responseText);
            dangerAlert.show().delay(4000);
            dangerAlert.fadeOut();
        });
        debugger;
    }

    /*successAlert.css('margin-top', '10px');
    successAlert.css('text-align', 'center');
    successAlert.show().delay(2000);
    successAlert.fadeOut();*/

});


/*Invest A Project*/


/*flag = false;

$(window).click(function (e) {
     //Hide the menus if visible onclick out of element
    if (e.target.className == 'js-back-pop-up') {
        backpopup.hide();
        flag = false;
     }
});

$('.js-back-x-btn').on('click', () => {
    if (!flag) {
        backpopup.show();
        flag = true;
    } else {
        backpopup.hide();
        flag = false;
    }
});

$('.js-back-btn').on('click', () => {
    if (!flag) {
        backpopup.show();
        flag = true;
    } else {
        flag = false;     
    }
});

*/

/*Invest A Project*/

btn = $('.js-submit-invest-withoutpack').on('click', () => {

    let clientId = parseInt($('.js-invest-wo-pack-cid').val());
    let investedAmount = parseInt($('.js-invest-wo-pack-amount').val());
    let projectId = parseInt($('.js-ProjectId').val());


    let data = {
        clientId: clientId,
        investedAmount: investedAmount,
        projectId: projectId
    }
    $.ajax({
        type: 'Post',
        url: '/InvestProject/Invest/',
        contentType: 'application/json',
        data: JSON.stringify(data)
    }).done(invest => {
        $('#exampleModal').modal('hide');
        successAlert.css('margin-top', '10px');
        successAlert.css('text-align', 'center');
        successAlert.show().delay(2000);
        successAlert.fadeOut();
        setTimeout(function () {
            window.location.assign("/Project/Details/" + projectId);
        }, 3000);
    }).fail(failureResponse => {
        $('#exampleModal').modal('hide');
        dangerAlert.css('margin-top', '10px');
        dangerAlert.css('text-align', 'center');
        dangerAlert.html(failureResponse.responseText);
        dangerAlert.show().delay(2000);
        dangerAlert.fadeOut();
        setTimeout(function () {
            window.location.assign("/Project/Details/" + projectId);
        }, 3000);
    });
});

/* Back with pack */

for (var i = 1; i <= $('.js-packages').val(); i++) {
    let packageId = parseInt($('.js-PackId_'+i).val());

    btn = $('.js-submit-invest-withpack_'+i).on('click', () => {

        let clientId = parseInt($('.js-invest-wo-pack-cid').val());
        let projectId = parseInt($('.js-ProjectId').val());
        
        let data = {
            clientId: clientId,
            projectId: projectId,
            packageId: packageId
        }
        $.ajax({
            type: 'Post',
            url: '/InvestProject/Invest/',
            contentType: 'application/json',
            data: JSON.stringify(data)
        }).done(invest => {
            $('#exampleModal').modal('hide');
            successAlert.css('margin-top', '10px');
            successAlert.css('text-align', 'center');
            successAlert.show().delay(2000);
            successAlert.fadeOut();
            setTimeout(function () {
                window.location.assign("/Project/Details/" + projectId);
            }, 3000);
        }).fail(failureResponse => {
            $('#exampleModal').modal('hide');
            dangerAlert.css('margin-top', '10px');
            dangerAlert.css('text-align', 'center');
            dangerAlert.html(failureResponse.responseText);
            dangerAlert.show().delay(2000);
            dangerAlert.fadeOut();
            setTimeout(function () {
                window.location.assign("/Project/Details/" + projectId);
            }, 3000);
        });
    });
}



/*Update Client*/

btn = $('#js-submit-customer-edit')
    .on('click', () => {

    let ClientId = $('.js-ClientId').val();
    let firstname = $('.js-firstname');
    let lastname = $('.js-lastname');
    let email = $('.js-email');
    let phone = $('.js-phone');
    let active = $('#DisableClientCheckBox');
        
    if (active.is(':checked')) {
        active = false;
    } else {
        active = true;
    }

    let data = {
        firstname: firstname.val(),
        lastname: lastname.val(),
        phone: phone.val(),
        email: email.val(),
        isActive: active
    }


    $.ajax({
        type: 'Patch',
        url: '/Client/Update/'+ClientId,
        contentType: 'application/json',
        data: JSON.stringify(data)
    }).done(update => {
        $('.form-control').removeClass('shadow bg-white rounded');
        successAlert.css('margin-top', '10px');
        successAlert.css('text-align', 'center');
        successAlert.show().delay(2000);
        successAlert.fadeOut();
        setTimeout(function () {
            window.location.assign("/Client/Details/" + ClientId);
        },3000);
    }).fail(failureResponse => {
        $('.input-validation-error').addClass('shadow bg-white rounded');
        dangerAlert.css('margin-top', '10px');
        dangerAlert.css('text-align', 'center');
        dangerAlert.html(failureResponse.responseText);
        dangerAlert.show().delay(2000);
        dangerAlert.fadeOut();
    });

});




/* PROJECTS */
/* Search Projects By Filters */


btn = $('#SearchProjectsByFilters').on('click', () => {
    let title = $('#inputSearchTitle');
    let description = $('#inputSearchDescription');
    let category = parseInt($('#searchControlSelectCategory').val());

    window.location.assign('/Project?Title=' + title.val() + '&Description=' + description.val() + '&Category=' + category);
});

let btn3 = $('#inputSearchDescription').on('keypress', (e) => {
    let title = $('#inputSearchTitle');
    let description = $('#inputSearchDescription');
    let category = parseInt($('#searchControlSelectCategory').val());

    if (e.keyCode==13) {
        window.location.assign('/Project?Title=' + title.val() + '&Description=' + description.val() + '&Category=' + category);
    }
});

let btn4 = $('#inputSearchTitle').on('keypress', (e) => {
    let title = $('#inputSearchTitle');
    let description = $('#inputSearchDescription');
    let category = parseInt($('#searchControlSelectCategory').val());

    if (e.keyCode==13) {
        window.location.assign('/Project?Title=' + title.val() + '&Description=' + description.val() + '&Category=' + category);
    }
});

let btn5 = $('#searchControlSelectCategory').on('keypress', (e) => {
    let title = $('#inputSearchTitle');
    let description = $('#inputSearchDescription');
    let category = parseInt($('#searchControlSelectCategory').val());

    if (e.keyCode==13) {
        window.location.assign('/Project?Title=' + title.val() + '&Description=' + description.val() + '&Category=' + category);
    }
});



/*  Update Project Details  */

btn = $('#js-submit-project-edit')
    .on('click', () => {

        let projectId = $('.js-ProjectId').val();
        let title = $('.js-title');
        let description = $('.js-description');
        let postStatusUpdates= $('.js-PostStatusUpdates');
 
        let data = {
            title: title.val(),
            description: description.val(),
            postStatusUpdates: postStatusUpdates.val()
        }

        $.ajax({
            type: 'Patch',
            url: '/Project/Update/' + projectId,
            contentType: 'application/json',
            data: JSON.stringify(data)
        }).done(update => {
            successAlert.css('margin-top', '10px');
            successAlert.css('text-align', 'center');
            successAlert.show().delay(2000);
            successAlert.fadeOut();
            setTimeout(function () {
                window.location.assign("/Project/Details/" + projectId);
            }, 3000);
        }).fail(failureResponse => {
            dangerAlert.css('margin-top', '10px');
            dangerAlert.css('text-align', 'center');
            dangerAlert.html(failureResponse.responseText);
            dangerAlert.show().delay(2000);
            dangerAlert.fadeOut();
        });

    });