if (variable_global_exists("getsomeActive") && global.getsomeActive)
{
    instance_destroy(getVbutton)
    getVbutton = scr_adaptiveTakeAllButtonCreate(id, (depth - 1), 230, 69, 1)
    with (getVbutton)
        owner = other.id
}
