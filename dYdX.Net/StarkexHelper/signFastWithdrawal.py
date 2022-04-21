from starkex.conditional_transfer import SignableConditionalTransfer
from starkex.helpers import get_transfer_erc20_fact
from starkex.helpers import nonce_from_client_id
import dateutil.parser as dp
import sys

ASSET_USDC = 'USDC'
COLLATERAL_ASSET = ASSET_USDC
NETWORK_ID_MAINNET = 1
NETWORK_ID_ROPSTEN = 3
FACT_REGISTRY_CONTRACT = {
    NETWORK_ID_MAINNET: '0xBE9a129909EbCb954bC065536D2bfAfBd170d27A',
    NETWORK_ID_ROPSTEN: '0x8Fb814935f7E63DEB304B500180e19dF5167B50e',
}
TOKEN_CONTRACTS = {
    ASSET_USDC: {
        NETWORK_ID_MAINNET: '0xA0b86991c6218b36c1d19D4a2e9Eb0cE3606eB48',
        NETWORK_ID_ROPSTEN: '0x8707A5bf4C2842d46B31A405Ba41b858C0F876c4',
    },
}
COLLATERAL_TOKEN_DECIMALS = 6

fact = get_transfer_erc20_fact(
                recipient=sys.argv[6],
                token_decimals=COLLATERAL_TOKEN_DECIMALS,
                human_amount=sys.argv[4],
                token_address=(
                    TOKEN_CONTRACTS[COLLATERAL_ASSET][int(sys.argv[1])]
                ),
                salt=nonce_from_client_id(sys.argv[3]),
            )
transfer_to_sign = SignableConditionalTransfer(
    network_id=int(sys.argv[1]),
    sender_position_id=sys.argv[2],
    receiver_position_id=sys.argv[7],
    receiver_public_key=sys.argv[8],
    fact_registry_address=FACT_REGISTRY_CONTRACT[int(sys.argv[1])],
    fact=fact,
    human_amount=sys.argv[5],
    client_id=sys.argv[3],
    expiration_epoch_seconds=dp.parse(sys.argv[9]).timestamp(),
)
signature = transfer_to_sign.sign(sys.argv[10])
print(signature)