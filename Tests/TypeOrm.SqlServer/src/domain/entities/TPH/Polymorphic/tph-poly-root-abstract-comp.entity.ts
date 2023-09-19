import { Entity, PrimaryGeneratedColumn, Column } from 'typeorm';

@Entity('tph_poly_root_abstract_comp')
export class TphPoly_RootAbstract_Comp {
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