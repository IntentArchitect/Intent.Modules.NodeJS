import { Entity, PrimaryGeneratedColumn, Column } from 'typeorm';

@Entity('tpc_poly_root_abstract_comp')
export class TpcPoly_RootAbstract_Comp {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  compField: string;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}