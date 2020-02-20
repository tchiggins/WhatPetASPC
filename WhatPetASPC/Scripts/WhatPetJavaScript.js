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

// Check if the Q3 answer is Yes or No and show the Q3b field if Yes
function checkQ3FieldResult()
{
    var result = document.getElementById("Q3").value;
    if (result == "Yes")
    {
        ShowQuestion("Q3bBlock");
        HideQuestion("Q4Block");
    }
    else
    {
        ShowQuestion("Q4Block");
        HideQuestion("Q3bBlock");
    }
};

// Check if the Q4 answer is Yes or No and show the Q4b field if Yes
function checkQ4FieldResult()
{
    var result = document.getElementById("Q4").value;
    if (result == "Yes")
    {
        ShowQuestion("Q4bBlock");
        HideQuestion("Q5Block");
    }
    else
    {
        ShowQuestion("Q5Block");
        HideQuestion("Q4bBlock");
    }
};

// Check if the Q9 answer is Yes or No and show the Q9b field if Yes
function checkQ9FieldResult()
{
    var result = document.getElementById("Q9").value;
    if (result == "Yes")
    {
        ShowQuestion("Q9bBlock");
        HideQuestion("Q10Block");
    }
    else
    {
        ShowQuestion("Q10Block");
        HideQuestion("Q9bBlock");
    }
};

// Check if the Q10 answer is Yes or No and show the Q10b field if Yes
function checkQ10FieldResult()
{
        var result = document.getElementById("Q10").value;
    if (result == "Yes")
    {
        ShowQuestion("Q10bBlock");
        HideQuestion("Submit");
    }
    else
    {
        ShowQuestion("Submit");
        HideQuestion("Q10bBlock");
    }
};

// The submit button has been pressed so copy the values to the hidden form fields
function SubmitPressed()
{
    document.getElementById("Q1Val").value = document.getElementById("Q1").value;
    document.getElementById("Q2Val").value = document.getElementById("Q2").value;
    document.getElementById("Q3Val").value = document.getElementById("Q3").value;
    document.getElementById("Q3bVal").value = document.getElementById("Q3b").value;
    document.getElementById("Q4Val").value = document.getElementById("Q4").value;
    document.getElementById("Q4bVal").value = document.getElementById("Q4b").value;
    document.getElementById("Q5Val").value = document.getElementById("Q5").value;
    document.getElementById("Q6Val").value = document.getElementById("Q6").value;
    document.getElementById("Q7Val").value = document.getElementById("Q7").value;
    document.getElementById("Q8Val").value = document.getElementById("Q8").value;
    document.getElementById("Q9Val").value = document.getElementById("Q9").value;
    document.getElementById("Q9bVal").value = document.getElementById("Q9b").value;
    document.getElementById("Q10Val").value = document.getElementById("Q10").value;
    document.getElementById("Q10bVal").value = document.getElementById("Q10b").value;
};

$("body").on("change", "#Q1", function ()
{
    ShowQuestion("Q2Block");
});

$("body").on("change", "#Q2", function ()
{
    ShowQuestion("Q3Block");
});

$("body").on("change", "#Q3", function ()
{
    checkQ3FieldResult();
});

$("body").on("change", "#Q3b", function ()
{
    ShowQuestion("Q4Block");
});

$("body").on("change", "#Q4", function ()
{
    checkQ4FieldResult();
});

$("body").on("change", "#Q4b", function ()
{
    ShowQuestion("Q5Block");
});

$("body").on("change", "#Q5", function ()
{
    ShowQuestion("Q6Block");
});

$("body").on("change", "#Q6", function ()
{
    ShowQuestion("Q7Block");
});

$("body").on("change", "#Q7", function ()
{
    ShowQuestion("Q8Block");
});

$("body").on("change", "#Q8", function ()
{
    ShowQuestion("Q9Block");
});

$("body").on("change", "#Q9", function ()
{
    checkQ9FieldResult();
});

$("body").on("change", "#Q9b", function ()
{
    ShowQuestion("Q10Block");
});

$("body").on("change", "#Q10", function ()
{
    checkQ10FieldResult();
});

$("body").on("change", "#Q10b", function ()
{
    ShowQuestion("Submit");
});
