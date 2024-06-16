function scr_msl_debug(argument0)
{
    var time = date_datetime_string(date_current_datetime());
    if (global._msl_log != noone)
    {
        if (global._msl_log.buf != noone)
        {
            var string_log = "[" + time + "]: " + argument0 + "\n";
            var len_log = string_byte_length(string_log);

            if (len_log > global._msl_log.size)
            {
                string_log = string_copy(string_log, 1, global._msl_log.size - 1) + "\n";
                len_log = string_byte_length(string_log);
            }

            if (len_log + global._msl_log.cur_size > global._msl_log.size)
            {
                scr_msl_log_save();
                global._msl_log.nfile += 1;
                global._msl_log.cur_size = 0;
            }

            buffer_write(global._msl_log.buf, buffer_text, string_log);
            global._msl_log.cur_size += len_log;

            var nfile_name = global._msl_log.name + "_" + string(global._msl_log.nfile) + ".txt";
            buffer_save_async(global._msl_log.buf, nfile_name, 0, global._msl_log.cur_size);
        }
        else
        {
            scr_actionsLogUpdate("msl log buff does not exist. Please report that bug to the MSL devs.");
        }
    }
    else
    {
        scr_actionsLogUpdate("msl log does not exist. Please report that bug to the MSL devs.");
    }
}