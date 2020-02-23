// Show the question
function ShowQuestion(ElementID)
{
    document.getElementById(ElementID).style.visibility = "visible";
};

// Hide the question
function HideQuestion(ElementID)
{
    document.getElementById(ElementID).style.visibility = "hidden";
};

// Check if we need to show a values pull-down
function checkFieldResult(Q, Qb, Next)
{
    var result = document.getElementById(Q).value;
    if (result == "Yes")
    {
        ShowQuestion(Qb);
        HideQuestion(Next);
    }
    else
    {
        ShowQuestion(Next);
        HideQuestion(Qb);
    }
};

// The submit button has been pressed so copy the values to the hidden form fields
function SubmitPressed()
{
    document.getElementById("QPetsAllowedVal").value = document.getElementById("QPetsAllowed").value;
    document.getElementById("QGardenVal").value = document.getElementById("QGarden").value;
    document.getElementById("QAllergiesVal").value = document.getElementById("QAllergies").value;
    document.getElementById("QAllergiesbVal").value = document.getElementById("QAllergiesb").value;
    document.getElementById("QPurchaseCostVal").value = document.getElementById("QPurchaseCost").value;
    document.getElementById("QMonthlyCostVal").value = document.getElementById("QMonthlyCost").value;
    document.getElementById("QHoursNeededVal").value = document.getElementById("QHoursNeeded").value;
    document.getElementById("QSizeVal").value = document.getElementById("QSize").value;
    document.getElementById("QSpeciesWantedVal").value = document.getElementById("QSpeciesWanted").value;
    document.getElementById("QSpeciesWantedbVal").value = document.getElementById("QSpeciesWantedb").value;
};

$("body").on("change", "#QPetsAllowed",     function () { ShowQuestion("QGardenBlock"); });
$("body").on("change", "#QGarden",         function () { ShowQuestion("QAllergiesBlock"); });
$("body").on("change", "#QAllergies",       function () { checkFieldResult("QAllergies", "QAllergiesbBlock", "QPurchaseCostBlock"); });
$("body").on("change", "#QAllergiesb",      function () { ShowQuestion("QPurchaseCostBlock"); });
$("body").on("change", "#QPurchaseCost",    function () { ShowQuestion("QMonthlyCostBlock"); });
$("body").on("change", "#QMonthlyCost",     function () { ShowQuestion("QHoursNeededBlock"); });
$("body").on("change", "#QHoursNeeded",     function () { ShowQuestion("QSizeBlock"); });
$("body").on("change", "#QSize",            function () { ShowQuestion("QSpeciesWantedBlock"); });
$("body").on("change", "#QSpeciesWanted", function () { checkFieldResult("QSpeciesWanted", "QSpeciesWantedbBlock", "Submit"); });
$("body").on("change", "#QSpeciesWantedb", function () { ShowQuestion("Submit"); });
