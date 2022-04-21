def strip_hex_prefix(input):
    if input.startswith('0x'):
        return input[2:]

    return input