import { useEffect, useState } from "react";
import { getRole, getRoles } from "../../services/role/roleService";
import type { RoleDto, RoleSummaryDto } from "../../types/role";
import RoleList from "../../components/roles/RoleList";
import PermissionMatrix from "../../components/roles/PermissionMatrix";

const Roles = () => {
  const [roles, setRoles] = useState<RoleSummaryDto[]>([]);
      const [selectedRole, setSelectedRole] = useState<RoleDto | null>(null);
  
      useEffect(() => {
          loadRoles();
      }, []);
  
      async function loadRoles() {
          const data = await getRoles();
  
          setRoles(data);
  
      }
  
      async function loadRole(roleName: string) {
          const role = await getRole(roleName);
          setSelectedRole(role);
      }
  return (
    <>
        <div className="mb-6">
            <h1 className="text-2xl font-bold">
                Roles Management
            </h1>
        </div>

        <div className="grid grid-cols-1 gap-6 lg:grid-cols-4">
            <div className="lg:col-span-1">
                <RoleList
                    roles={roles}
                    selectedRole={selectedRole?.name ?? ""}
                    onSelect={loadRole}
                />
            </div>

            <div className="lg:col-span-3">
                <PermissionMatrix role={selectedRole} />
            </div>
        </div>
    </>

  );
};

export default Roles;