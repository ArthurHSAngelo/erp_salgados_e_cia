/* =====================================================================
   ERP SALGADOS & CIA - Acesso ao banco pela rede (OPCIONAL)

   O 01_schema.sql cria o usuario de aplicacao 'app_salgados' so para a propria
   maquina (localhost). Este script cria o MESMO usuario, com as mesmas
   permissoes, para as outras unidades acessarem o banco pela rede
   (requisito de banco centralizado compartilhado entre as unidades).

   Quando usar: no computador que guarda o banco (o servidor), depois de
   rodar o 01_schema.sql e o 02_dados_teste.sql.

   Antes de rodar:
   1. Troque a senha 'TROQUE_ESTA_SENHA_FORTE' por uma senha forte (use a mesma
      que sera informada na instalacao do programa nos outros computadores).
   2. Troque o '%' (qualquer computador) pela faixa de enderecos da sua rede
      local, se souber, por exemplo '192.168.1.%'. Assim so computadores dessa
      rede conseguem conectar.
   3. No servidor, libere a porta 3306 do MySQL no firewall do Windows e confira
      que o MySQL aceita conexoes de rede (no XAMPP/MariaDB: "bind-address"
      no my.ini nao pode estar restrito a 127.0.0.1).

   Cuidado: abrir o banco para a rede e uma decisao de seguranca. Use apenas em
   rede de confianca. O script e seguro para rodar mais de uma vez.
   ===================================================================== */

CREATE USER IF NOT EXISTS 'app_salgados'@'%' IDENTIFIED BY 'TROQUE_ESTA_SENHA_FORTE';

GRANT SELECT  ON salgados_cia.* TO 'app_salgados'@'%';
-- cadastros: podem ser criados, alterados e (sem historico) excluidos
GRANT INSERT, UPDATE, DELETE ON salgados_cia.unidade     TO 'app_salgados'@'%';
GRANT INSERT, UPDATE, DELETE ON salgados_cia.produto     TO 'app_salgados'@'%';
GRANT INSERT, UPDATE, DELETE ON salgados_cia.fornecedor  TO 'app_salgados'@'%';
GRANT INSERT, UPDATE, DELETE ON salgados_cia.funcionario TO 'app_salgados'@'%';
GRANT INSERT, UPDATE, DELETE ON salgados_cia.cliente     TO 'app_salgados'@'%';
-- logins: criados e inativados, nunca excluidos
GRANT INSERT, UPDATE ON salgados_cia.usuario             TO 'app_salgados'@'%';
-- comanda: o app cria e pode trocar mesa/cliente; o STATUS so muda pelas procedures
GRANT INSERT ON salgados_cia.comanda                     TO 'app_salgados'@'%';
GRANT UPDATE (numero_mesa, cliente_id) ON salgados_cia.comanda TO 'app_salgados'@'%';
-- historico financeiro/estoque: so INSERT (nada de UPDATE/DELETE)
GRANT INSERT ON salgados_cia.item_comanda                TO 'app_salgados'@'%';
GRANT INSERT ON salgados_cia.pagamento_comanda           TO 'app_salgados'@'%';
GRANT INSERT ON salgados_cia.abatimento_notinha          TO 'app_salgados'@'%';
GRANT INSERT ON salgados_cia.entrada_estoque             TO 'app_salgados'@'%';
GRANT INSERT ON salgados_cia.perda                       TO 'app_salgados'@'%';
-- inventario: a contagem pode ser corrigida enquanto e preenchida (ate ser efetivada)
GRANT INSERT, UPDATE ON salgados_cia.inventario          TO 'app_salgados'@'%';
GRANT INSERT, UPDATE ON salgados_cia.item_inventario     TO 'app_salgados'@'%';
GRANT EXECUTE ON salgados_cia.*                          TO 'app_salgados'@'%';
FLUSH PRIVILEGES;

-- Conferir:  SHOW GRANTS FOR 'app_salgados'@'%';
